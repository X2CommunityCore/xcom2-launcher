using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCOM2Launcher.Mod;

namespace XCOM2Launcher.Classes.Mod
{
    /// <summary>
    /// A class used for the purpose of data binding to object property view, controlling which field can be edited and handle any
    /// code needed when a field is edited.
    /// </summary>
    public class ModProperty : INotifyPropertyChanged
    { 
        [Browsable(false)]
        private ModEntry modEntry;

        public event PropertyChangedEventHandler PropertyChanged;
        
        [Category("Mod Status")]
        public int Index
        {
            get { return modEntry.Index; }
            set {
                modEntry.Index = value;

                PropertyChangedEventArgs e = new PropertyChangedEventArgs("Index");
                PropertyChanged(this, e);
            }
        }

        [Category("Mod Status")]
        public string State {
            get
            {
                if (modEntry.State == ModState.None)
                    return "None";

                List<string> states = new List<string>();

                foreach (ModState st in Enum.GetValues(typeof(ModState)))
                {
                    if ((modEntry.State & st) != ModState.None)
                    {
                        states.Add(Enum.GetName(typeof(ModState), st));
                    }
                }

                return String.Join(", ", states);
            }
        }

        [Category("Mod Info")]
        public string ID { get { return modEntry.ID; } }

        [Category("Mod Info")]
        public string Name
        {
            get { return modEntry.Name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    modEntry.Name = "";
                    modEntry.ManualName = false;
                    Settings.Instance.Mods.UpdateMod(modEntry, Settings.Instance);
                    PropertyChangedEventArgs e = new PropertyChangedEventArgs("Name");
                    PropertyChanged(this, e);
                }
                else if (!value.Equals(modEntry.Name))
                {
                    modEntry.Name = value;
                    modEntry.ManualName = true;
                    PropertyChangedEventArgs e = new PropertyChangedEventArgs("Name");
                    PropertyChanged(this, e);
                }
            }
        }

        [Category("Mod Info")]
        public bool ManualName { get { return modEntry.ManualName; } }

        [Category("Mod Info")]
        public string Author
        {
            get { return modEntry.Author; }
            set {
                modEntry.Author = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("Author");
                PropertyChanged(this, e);
            }
        }

        [Category("Mod Info")]
        public string Description
        {
            get { return modEntry.Description; }
            set {
                modEntry.Description = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("Description");
                PropertyChanged(this, e);
            }
        }

        [Category("Mod Properties")]
        public string Path { get { return modEntry.Path; } }

        [Category("Mod Properties")]
        public long Size { get { return modEntry.Size; } }

        [Category("Mod Status")]
        public bool isActive
        {
            get { return modEntry.isActive; }
            set {
                modEntry.isActive = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("isActive");
                PropertyChanged(this, e);
            }
        }

        [Category("Mod Status")]
        public bool isHidden
        {
            get { return modEntry.isHidden; }
            set {
                modEntry.isHidden = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("isHidden");
                PropertyChanged(this, e);
            }
        }

        [Category("Mod Properties")]
        public ModSource Source { get { return modEntry.Source; } }

        [Category("Mod Properties")]
        public long WorkshopID { get { return modEntry.WorkshopID; } }

        [Category("Mod Properties")]
        public string DateAdded { get { return modEntry.DateAdded.HasValue ? modEntry.DateAdded.Value.ToString() : "Unknown"; } }
        [Category("Mod Properties")]
        public string DateCreated { get { return modEntry.DateCreated.HasValue ? modEntry.DateCreated.Value.ToString() : "Unknown"; } }
        [Category("Mod Properties")]
        public string DateUpdated { get { return modEntry.DateUpdated.HasValue ? modEntry.DateUpdated.Value.ToString() : "Unknown"; } }

        [Category("Mod Info")]
        public string Note
        {
            get { return modEntry.Note; }
            set { modEntry.Note = value; }
        }

        [Category("Mod Properties")]
        public bool HasBackedUpSettings => modEntry.Settings.Count > 0;

        [Category("Mod Properties")]
        public string Image
        {
            get { return modEntry.Image; }
            set { modEntry.Image = value; }
        }

        [Category("Mod Info")]
        public string Tags => String.Join(", ", modEntry.Tags);

        [Category("Mod Properties")]
        public string SteamLink => modEntry.GetSteamLink();
        [Category("Mod Properties")]
        public string BrowserLink => modEntry.GetWorkshopLink();

        [Category("Mod Properties")]
        public bool BuiltForWOTC { get { return modEntry.BuiltForWOTC; } }

        public ModProperty(ModEntry modEntry)
        {
            this.modEntry = modEntry;
        }
    }
}
