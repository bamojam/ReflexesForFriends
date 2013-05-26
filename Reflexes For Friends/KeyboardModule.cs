using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SFML.Window;
using System.Text;

namespace Reflexes_For_Friends
{
    class KeyboardModule
    {
        Dictionary<string, Action> keyBindings;

        public KeyboardModule()
        {
            keyBindings = new Dictionary<string, Action>();
        }

        public void ProcessKeyEvent(KeyEventArgs pressedKey, bool keyPressEvent)
        {
            string currentEventHash = GenerateKeyBindingCode(pressedKey, keyPressEvent);
            if (keyBindings.ContainsKey(currentEventHash))
                keyBindings[currentEventHash]();
        }

        /// <summary>
        /// Generates a key binding code using the passed keyEvent and keyPressEvent values.
        /// Then links that code with the passed action.
        /// </summary>
        /// <param name="keyEvent">The key and modifiers that represent this key binding.</param>
        /// <param name="keyPressEvent">True if this should trigger on a KeyPress event; False if
        /// it should trigger on a KeyRelease event</param>
        /// <param name="action">Action to be linked with the key binding code.</param>
        public void AddBinding(KeyEventArgs keyEvent, bool keyPressEvent, Action action)
        {
            keyBindings.Add(GenerateKeyBindingCode(keyEvent, keyPressEvent), action);
        }

        /// <summary>
        /// Not Yet Implemented. Potentially to be done later.
        /// </summary>
        /// <param name="keyEvents"></param>
        /// <param name="keyPressEvents"></param>
        /// <param name="actions"></param>
        public void AddBindings(IEnumerable<KeyEventArgs> keyEvents, IEnumerable<bool> keyPressEvents, IEnumerable<Action> actions)
        {
        }

        static public string GenerateKeyBindingCode(KeyEventArgs keyEvent, bool keyPress)
        {
            var code = new StringBuilder();

            code.Append(keyPress ? '1' : '0');
            code.Append(keyEvent.Alt ? '1' : '0');
            code.Append(keyEvent.Control ? '1' : '0');
            code.Append(keyEvent.Shift ? '1' : '0');
            code.Append(keyEvent.System ? '1' : '0');
            code.Append(keyEvent.Code.ToString());

            return code.ToString();
        }
    }
}
