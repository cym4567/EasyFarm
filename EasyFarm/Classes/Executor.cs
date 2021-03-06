﻿
/*///////////////////////////////////////////////////////////////////
<EasyFarm, general farming utility for FFXI.>
Copyright (C) <2013>  <Zerolimits>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
*/
///////////////////////////////////////////////////////////////////

using EasyFarm.UserSettings;
using FFACETools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EasyFarm.Classes
{
    /// <summary>
    /// Executor targeted or buffing actions. A fuller explanation 
    /// can be found here: https://github.com/EasyFarm/EasyFarm/wiki/Battle-List-Roles. 
    /// See "list types" for more details. 
    /// </summary>
    public class Executor
    {
        #region Member Variables
        private readonly FFACE FFACE;
        private readonly Caster Caster;
        #endregion

        #region Constructors
        public Executor(FFACE fface)
        {
            this.FFACE = fface;
            this.Caster = new Caster(fface);
        }
        #endregion

        #region Buffing Methods
        /// <summary>
        /// Executes moves without the need for a target. 
        /// </summary>
        /// <param name="actions"></param>
        public void UseBuffingActions(IEnumerable<BattleAbility> actions)
        {
            if (actions == null) throw new ArgumentNullException("actions");

            foreach (var action in actions.ToList())
            {
                // Cast the spell. 
                Caster.CastSpell(action);

                // Sleep until a spell is recastable. 
                Thread.Sleep(Config.Instance.GlobalCooldown);
            }
        }

        /// <summary>
        /// Execute actions that are not target oriented. 
        /// </summary>
        /// <param name="actions"></param>
        public void UseBuffingActions(IEnumerable<Ability> actions)
        {
            if (actions == null) throw new ArgumentNullException("actions");
            UseBuffingActions(actions.Select(x => new BattleAbility() { Ability = x }));
        }

        /// <summary>
        /// Execute a signle buffing type action. 
        /// </summary>
        /// <param name="action"></param>
        public void UseBuffingAction(BattleAbility action)
        {
            if (action == null) throw new ArgumentNullException("action");
            UseBuffingActions(new List<BattleAbility>() { action });
        }

        /// <summary>
        /// Execute a single buffing type action. 
        /// </summary>
        /// <param name="action"></param>
        public void UseBuffingAction(Ability action)
        {
            if (action == null) throw new ArgumentNullException("action");
            UseBuffingActions(new List<BattleAbility>() { new BattleAbility() { Ability = action } });
        }
        #endregion

        #region Targeted Methods
        /// <summary>
        /// Execute targeted actions. 
        /// </summary>
        /// <param name="actions"></param>
        public void UseTargetedActions(IEnumerable<BattleAbility> actions, Unit target)
        {
            // Logic error to call this without setting a target first. 
            if (actions == null) throw new ArgumentNullException("actions");
            if (target == null) throw new ArgumentNullException("target");

            foreach (var action in actions)
            {
                // Move to target if out of distance. 
                if (target.Distance > action.Distance)
                {
                    // Move to unit at max buff distance. 
                    var oldTolerance = FFACE.Navigator.DistanceTolerance;
                    FFACE.Navigator.DistanceTolerance = action.Distance;
                    FFACE.Navigator.GotoNPC(target.ID);
                    FFACE.Navigator.DistanceTolerance = action.Distance;
                }

                // Face unit
                FFACE.Navigator.FaceHeading(target.Position);

                // Target mob if not currently targeted. 
                if (target.ID != FFACE.Target.ID)
                {
                    FFACE.Target.SetNPCTarget(target.ID);
                    FFACE.Windower.SendString("/ta <t>");
                }

                if (action.Ability.ActionType == ActionType.Spell)
                {
                    Caster.CastSpell(action);
                    Thread.Sleep(Config.Instance.GlobalCooldown);
                }
                else
                {
                    Caster.CastAbility(action);
                }
            }
        }

        /// <summary>
        /// Execute a single action targeted type action. 
        /// </summary>
        /// <param name="action"></param>
        public void UseTargetedAction(BattleAbility action, Unit target)
        {
            if (target == null) throw new ArgumentNullException("target");
            if (action == null) throw new ArgumentNullException("action");
            UseTargetedActions(new List<BattleAbility>() { action }, target);
        }
        #endregion
    }
}
