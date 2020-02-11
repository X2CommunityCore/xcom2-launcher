using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrightIdeasSoftware;
using XCOM2Launcher.Mod;

namespace XCOM2Launcher.Classes.Mod
{
    class ModListFilter : TextMatchFilter
    {
        private readonly List<ModState> _States;
        private readonly bool _ShowHiddenMods;

        public ModListFilter(ObjectListView olv, string text, List<ModState> states, bool showHiddenMods) : base(olv, text)
        {
            _States = states;
            _ShowHiddenMods = showHiddenMods;
        }

        public override bool Filter(object modelObject)
        {
            if (!(modelObject is ModEntry mod))
            {
                return true;
            }

            bool textMatch = base.Filter(mod);

            bool isAdditionalFilterActive = false;
            bool filterMatch = false;
            var tempStates = new List<ModState>(_States);

            // MissingDependencies gets special processing
            if (tempStates.Contains(ModState.MissingDependencies))
            {
                tempStates.Remove(ModState.MissingDependencies);
                isAdditionalFilterActive = true;
                bool hasFlag = mod.State.HasFlag(ModState.MissingDependencies);
                filterMatch |= (mod.isActive && hasFlag);
            }

            if (tempStates.Any())
            {
                isAdditionalFilterActive = true;
                filterMatch |= tempStates.Any(flag => mod.State.HasFlag(flag));
            }

            if (_ShowHiddenMods)
            {
                isAdditionalFilterActive = true;
                filterMatch |= mod.isHidden;
            }

            if (!isAdditionalFilterActive)
            {
                return textMatch;
            }

            return filterMatch && textMatch;
        }
    }
}
