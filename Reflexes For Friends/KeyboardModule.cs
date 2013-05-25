using System;
using System.Collections;
using System.Collections.Generic;
using SFML.Window;

namespace Reflexes_For_Friends
{
    class KeyboardModule
    {
        private IList<KeyBinding> advancedKeyBindings;
        private IList<KeyBinding> basicKeyBindings;

        public KeyboardModule()
        {
            advancedKeyBindings = new List<KeyBinding>();
            basicKeyBindings = new List<KeyBinding>();
        }

        public void ProcessKeyPress(KeyEventArgs pressedKey, bool keyPressEvent)
        {
            foreach (KeyBinding binding in advancedKeyBindings)
            {
                if (binding.Matches(pressedKey))
                {
                    return;
                }
            }

            foreach (KeyBinding binding in basicKeyBindings)
            {
                if (binding.Matches(pressedKey))
                {
                    return;
                }
            }
        }

        public void AdjustKeyBindings(IList keyBindings, bool keyBindingsIncludeModifiers, bool removeOldBindings)
        {

        }
    }
}
