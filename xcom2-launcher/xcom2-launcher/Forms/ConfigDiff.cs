using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using XCOM2Launcher.Forms.DiffMergeStuffs;
using XCOM2Launcher.XCOM;

namespace XCOM2Launcher.Forms
{
	public partial class ConfigDiff : Form
	{
		private static ConfigDiff _instanceConfigDiff;

		public static ConfigDiff Instance
		{
			get
			{
				if (_instanceConfigDiff == null || _instanceConfigDiff.IsDisposed)
					_instanceConfigDiff = new ConfigDiff();
				return _instanceConfigDiff;
			}
		}

		private int _updating;
		private readonly Style _greenStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Lime)));
		private readonly Style _redStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Red)));

		public string File1;

		public string File2;

		public ConfigDiff()
		{
			InitializeComponent();
		}

		public void CompareStrings(string s1, string s2)
		{
			File1 = s1;
			File2 = s2;
			Compare();
		}

		private void tb_VisibleRangeChanged(object sender, EventArgs e)
		{
			if (_updating > 0)
				return;

			var vPos = (sender as FastColoredTextBox).VerticalScroll.Value;
			var curLine = (sender as FastColoredTextBox).Selection.Start.iLine;

			if (sender == fctb2)
				UpdateScroll(fctb1, vPos, curLine);
			else
				UpdateScroll(fctb2, vPos, curLine);

			fctb1.Refresh();
			fctb2.Refresh();
		}

		private void UpdateScroll(FastColoredTextBox tb, int vPos, int curLine)
		{
			if (_updating > 0)
				return;
			//
			BeginUpdate();
			//
			if (vPos <= tb.VerticalScroll.Maximum)
			{
				tb.VerticalScroll.Value = vPos;
				tb.UpdateScrollbars();
			}

			if (curLine < tb.LinesCount)
				tb.Selection = new Range(tb, 0, curLine, 0, curLine);
			//
			EndUpdate();
		}

		private void EndUpdate()
		{
			_updating--;
		}

		private void BeginUpdate()
		{
			_updating++;
		}

		private void btCompare_Click(object sender, EventArgs e)
		{
			Compare();
		}

		private void Compare()
		{
			fctb1.Clear();
			fctb2.Clear();

			Cursor = Cursors.WaitCursor;

			var source1 = Lines.Load(File1);
			var source2 = Lines.Load(File2);

			source1.Merge(source2);

			BeginUpdate();

			Process(source1);

			EndUpdate();

			Cursor = Cursors.Default;
		}

		private void Process(Lines lines)
		{
			foreach (var line in lines)
			{
				switch (line.State)
				{
					case DiffType.None:
						fctb1.AppendText(line.line + Environment.NewLine);
						fctb2.AppendText(line.line + Environment.NewLine);
						break;
					case DiffType.Inserted:
						fctb1.AppendText(Environment.NewLine);
						fctb2.AppendText(line.line + Environment.NewLine, _greenStyle);
						break;
					case DiffType.Deleted:
						fctb1.AppendText(line.line + Environment.NewLine, _redStyle);
						fctb2.AppendText(Environment.NewLine);
						break;
				}
				if (line.SubLines != null)
					Process(line.SubLines);
			}
		}

		private void fctb_TextChanged(object sender, TextChangedEventArgs e)
		{
			IniLanguage.Process(e);
		}
	}

	#region Merge stuffs

	namespace DiffMergeStuffs
	{
		public class SimpleDiff<T>
		{
			private IList<T> _left;
			private IList<T> _right;
			private int[,] _matrix;
			private bool _matrixCreated;
			private int _preSkip;
			private int _postSkip;

			private Func<T, T, bool> _compareFunc;

			public SimpleDiff(IList<T> left, IList<T> right)
			{
				_left = left;
				_right = right;

				InitializeCompareFunc();
			}

			public event EventHandler<DiffEventArgs<T>> LineUpdate;

			public TimeSpan ElapsedTime { get; private set; }

			/// <summary>
			/// This is the sole public method and it initializes
			/// the LCS matrix the first time it's called, and 
			/// proceeds to fire a series of LineUpdate events
			/// </summary>
			public void RunDiff()
			{
				if (!_matrixCreated)
				{
					Stopwatch sw = new Stopwatch();
					sw.Start();
					CalculatePreSkip();
					CalculatePostSkip();
					CreateLCSMatrix();
					sw.Stop();
					this.ElapsedTime = sw.Elapsed;
				}

				for (int i = 0; i < _preSkip; i++)
				{
					FireLineUpdate(DiffType.None, i, -1);
				}

				int totalSkip = _preSkip + _postSkip;
				ShowDiff(_left.Count - totalSkip, _right.Count - totalSkip);

				int leftLen = _left.Count;
				for (int i = _postSkip; i > 0; i--)
				{
					FireLineUpdate(DiffType.None, leftLen - i, -1);
				}
			}

			/// <summary>
			/// This method is an optimization that
			/// skips matching elements at the end of the 
			/// two arrays being diff'ed.
			/// Care's taken so that this will never
			/// overlap with the pre-skip.
			/// </summary>
			private void CalculatePostSkip()
			{
				int leftLen = _left.Count;
				int rightLen = _right.Count;
				while (_postSkip < leftLen && _postSkip < rightLen &&
					   _postSkip < (leftLen - _preSkip) &&
					   _compareFunc(_left[leftLen - _postSkip - 1], _right[rightLen - _postSkip - 1]))
				{
					_postSkip++;
				}
			}

			/// <summary>
			/// This method is an optimization that
			/// skips matching elements at the start of
			/// the arrays being diff'ed
			/// </summary>
			private void CalculatePreSkip()
			{
				int leftLen = _left.Count;
				int rightLen = _right.Count;
				while (_preSkip < leftLen && _preSkip < rightLen &&
					   _compareFunc(_left[_preSkip], _right[_preSkip]))
				{
					_preSkip++;
				}
			}

			/// <summary>
			/// This traverses the elements using the LCS matrix
			/// and fires appropriate events for added, subtracted, 
			/// and unchanged lines.
			/// It's recursively called till we run out of items.
			/// </summary>
			/// <param name="leftIndex"></param>
			/// <param name="rightIndex"></param>
			private void ShowDiff(int leftIndex, int rightIndex)
			{
				if (leftIndex > 0 && rightIndex > 0 &&
					_compareFunc(_left[_preSkip + leftIndex - 1], _right[_preSkip + rightIndex - 1]))
				{
					ShowDiff(leftIndex - 1, rightIndex - 1);
					FireLineUpdate(DiffType.None, _preSkip + leftIndex - 1, -1);
				}
				else
				{
					if (rightIndex > 0 &&
						(leftIndex == 0 ||
						 _matrix[leftIndex, rightIndex - 1] >= _matrix[leftIndex - 1, rightIndex]))
					{
						ShowDiff(leftIndex, rightIndex - 1);
						FireLineUpdate(DiffType.Inserted, -1, _preSkip + rightIndex - 1);
					}
					else if (leftIndex > 0 &&
							 (rightIndex == 0 ||
							  _matrix[leftIndex, rightIndex - 1] < _matrix[leftIndex - 1, rightIndex]))
					{
						ShowDiff(leftIndex - 1, rightIndex);
						FireLineUpdate(DiffType.Deleted, _preSkip + leftIndex - 1, -1);
					}
				}

			}

			/// <summary>
			/// This is the core method in the entire class,
			/// and uses the standard LCS calculation algorithm.
			/// </summary>
			private void CreateLCSMatrix()
			{
				int totalSkip = _preSkip + _postSkip;
				if (totalSkip >= _left.Count || totalSkip >= _right.Count)
					return;

				// We only create a matrix large enough for the
				// unskipped contents of the diff'ed arrays
				_matrix = new int[_left.Count - totalSkip + 1, _right.Count - totalSkip + 1];

				for (int i = 1; i <= _left.Count - totalSkip; i++)
				{
					// Simple optimization to avoid this calculation
					// inside the outer loop (may have got JIT optimized 
					// but my tests showed a minor improvement in speed)
					int leftIndex = _preSkip + i - 1;

					// Again, instead of calculating the adjusted index inside
					// the loop, I initialize it under the assumption that
					// incrementing will be a faster operation on most CPUs
					// compared to addition. Again, this may have got JIT
					// optimized but my tests showed a minor speed difference.
					for (int j = 1, rightIndex = _preSkip + 1; j <= _right.Count - totalSkip; j++, rightIndex++)
					{
						_matrix[i, j] = _compareFunc(_left[leftIndex], _right[rightIndex - 1])
											? _matrix[i - 1, j - 1] + 1
											: Math.Max(_matrix[i, j - 1], _matrix[i - 1, j]);
					}
				}

				_matrixCreated = true;
			}

			private void FireLineUpdate(DiffType diffType, int leftIndex, int rightIndex)
			{
				var local = this.LineUpdate;

				if (local == null)
					return;

				T lineValue = leftIndex >= 0 ? _left[leftIndex] : _right[rightIndex];

				local(this, new DiffEventArgs<T>(diffType, lineValue, leftIndex, rightIndex));
			}

			private void InitializeCompareFunc()
			{
				// Special case for String types
				if (typeof(T) == typeof(String))
				{
					_compareFunc = StringCompare;
				}
				else
				{
					_compareFunc = DefaultCompare;
				}
			}

			/// <summary>
			/// This comparison is specifically
			/// for strings, and was nearly thrice as 
			/// fast as the default comparison operation.
			/// </summary>
			/// <param name="left"></param>
			/// <param name="right"></param>
			/// <returns></returns>
			private bool StringCompare(T left, T right)
			{
				return Object.Equals(left, right);
			}

			private bool DefaultCompare(T left, T right)
			{
				return left.Equals(right);
			}
		}

		[Flags]
		public enum DiffType
		{
			None = 0,
			Inserted = 1,
			Deleted = 2
		}

		public class DiffEventArgs<T> : EventArgs
		{
			public DiffType DiffType { get; set; }

			public T LineValue { get; private set; }
			public int LeftIndex { get; private set; }
			public int RightIndex { get; private set; }

			public DiffEventArgs(DiffType diffType, T lineValue, int leftIndex, int rightIndex)
			{
				this.DiffType = diffType;
				this.LineValue = lineValue;
				this.LeftIndex = leftIndex;
				this.RightIndex = rightIndex;
			}
		}

		/// <summary>
		/// Line of file
		/// </summary>
		public class Line
		{
		    protected bool Equals(Line other)
		    {
		        return string.Equals(line, other.line) && Equals(SubLines, other.SubLines) && State == other.State;
		    }

		    public override int GetHashCode()
		    {
		        unchecked
		        {
		            var hashCode = (line != null ? line.GetHashCode() : 0);
		            hashCode = (hashCode * 397) ^ (SubLines != null ? SubLines.GetHashCode() : 0);
		            hashCode = (hashCode * 397) ^ (int) State;
		            return hashCode;
		        }
		    }

		    /// <summary>
			/// Source string
			/// </summary>
			public readonly string line;

			/// <summary>
			/// Inserted strings
			/// </summary>
			public Lines SubLines;

			/// <summary>
			/// Line state
			/// </summary>
			public DiffType State;

			public Line(string line)
			{
				this.line = line;
			}

			/// <summary>
			/// Equals
			/// </summary>
			public override bool Equals(object obj)
			{
				return Object.Equals(line, ((Line)obj).line);
			}

			public static bool operator ==(Line line1, Line line2)
			{
				return Object.Equals(line1.line, line2.line);
			}

			public static bool operator !=(Line line1, Line line2)
			{
				return !Object.Equals(line1.line, line2.line);
			}

			public override string ToString()
			{
				return line;
			}
		}

		/// <summary>
		/// File as list of lines
		/// </summary>
		public class Lines : List<Line>, IEquatable<Lines>
		{
			//This line is necessary to store the rows inserted in the beginning, before the first line of the source file
			private Line _fictiveLine = new Line("===fictive line===") { State = DiffType.Deleted };

			public Lines()
			{
			}


			public Lines(int capacity)
				: base(capacity)
			{
			}

			public new Line this[int i]
			{
				get
				{
					if (i == -1) return _fictiveLine;
					return base[i];
				}

				set
				{
					if (i == -1) _fictiveLine = value;
					base[i] = value;
				}
			}

			/// <summary>
			/// Load from file
			/// </summary>
			public static Lines Load(string s)
			{
				Lines lines = new Lines();
				try
				{
					using (StringReader reader = new StringReader(s))
					{
						string line;
						while ((line = reader.ReadLine()) != null)
							lines.Add(new Line(line));
					}
				}
				catch (Exception) {}
				return lines;

			}

			/// <summary>
			/// Merge lines
			/// </summary>
			public void Merge(Lines lines)
			{
				SimpleDiff<Line> diff = new SimpleDiff<Line>(this, lines);
				int iLine = -1;

				diff.LineUpdate += (o, e) =>
				{
					if (e.DiffType == DiffType.Inserted)
					{
						if (this[iLine].SubLines == null)
							this[iLine].SubLines = new Lines();
						e.LineValue.State = DiffType.Inserted;
						this[iLine].SubLines.Add(e.LineValue);
					}
					else
					{
						iLine++;
						this[iLine].State = e.DiffType;
						if (iLine > 0 &&
							this[iLine - 1].State == DiffType.Deleted &&
							this[iLine - 1].SubLines == null &&
							e.DiffType == DiffType.None)
							this[iLine - 1].SubLines = new Lines();
					}
				};
				//запускаем алгоритм нахождения максимальной подпоследовательности (LCS)
				diff.RunDiff();
			}

			/// <summary>
			/// Clone
			/// </summary>
			public Lines Clone()
			{
				Lines result = new Lines(this.Count);
				foreach (var line in this)
					result.Add(new Line(line.line));

				return result;
			}

			/// <summary>
			/// Is lines equal?
			/// </summary>
			public bool Equals(Lines other)
			{
				if (Count != other.Count)
					return false;
				for (int i = 0; i < Count; i++)
					if (this[i] != other[i])
						return false;
				return true;
			}

			/// <summary>
			/// Transform tree to list
			/// </summary>
			public Lines Expand()
			{
				return Expand(-1, Count - 1);
			}

			/// <summary>
			/// Transform tree to list
			/// </summary>
			public Lines Expand(int from, int to)
			{
				Lines result = new Lines();
				for (int i = from; i <= to; i++)
				{
					if (this[i].State != DiffType.Deleted)
						result.Add(this[i]);
					if (this[i].SubLines != null)
						result.AddRange(this[i].SubLines.Expand());
				}

				return result;
			}
		}

		/// <summary>
		/// A string containing several conflicting versions
		/// </summary>
		public class ConflictedLine : Line
		{
			public readonly Lines version1;
			public readonly Lines version2;

			public ConflictedLine(Lines version1, Lines version2)
				: base("?")
			{
				this.version1 = version1;
				this.version2 = version2;
			}
		}
	}
	#endregion
}
