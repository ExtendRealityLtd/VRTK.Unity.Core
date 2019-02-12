﻿namespace Zinnia.Action
{
    using System.Linq;
    using System.Collections.Generic;
    using Malimbe.PropertySerializationAttribute;
    using Malimbe.PropertySetterMethod;
    using Malimbe.PropertyValidationMethod;
    using Malimbe.XmlDocumentationAttribute;

    /// <summary>
    /// Emits a <see cref="bool"/> value when all given actions are in their active state.
    /// </summary>
    public class AllAction : BooleanAction
    {
        /// <summary>
        /// Actions to check the active state on.
        /// </summary>
        [Serialized, Validated]
        [field: DocumentedByXml]
        public List<Action> Actions { get; set; } = new List<Action>();

        protected virtual void Update()
        {
            bool areAllActionsActivated = Actions?.Count > 0 && Actions.All(action => action.IsActivated);
            if (areAllActionsActivated != IsActivated)
            {
                Receive(areAllActionsActivated);
            }
        }

        /// <summary>
        /// Handles changes to <see cref="Actions"/>.
        /// </summary>
        /// <param name="previousValue">The previous value.</param>
        /// <param name="newValue">The new value.</param>
        [CalledBySetter(nameof(Actions))]
        protected virtual void OnActionsChange(List<Action> previousValue, ref List<Action> newValue)
        {
            Update();
        }
    }
}