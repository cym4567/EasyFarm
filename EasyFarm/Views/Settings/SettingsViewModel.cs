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

using EasyFarm.Classes;
using EasyFarm.UserSettings;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace EasyFarm.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            RestoreDefaultsCommand = new DelegateCommand(RestoreDefaults);
        }

        public ICommand RestoreDefaultsCommand { get; set; }
        
        private void RestoreDefaults()
        {
            DetectionDistance = Constants.DETECTION_DISTANCE;
            HeightThreshold = Constants.HEIGHT_THRESHOLD;
            MeleeDistance = Constants.MELEE_DISTANCE;
            WanderDistance = Constants.DETECTION_DISTANCE;
            CastLatency = Constants.SPELL_CAST_LATENCY;
            GlobalCooldown = Constants.GLOBAL_SPELL_COOLDOWN;
            InformUser("Defaults have been restored.");
        }

        public double DetectionDistance
        {
            get { return Config.Instance.DetectionDistance; }
            set 
            { 
                SetProperty<double>(ref Config.Instance.DetectionDistance, (int)value);
                InformUser("Detection Distance Set: {0}.", (int)value);
            }
        }

        public double HeightThreshold
        {
            get { return Config.Instance.HeightThreshold; }
            set
            {
                SetProperty<double>(ref Config.Instance.HeightThreshold, value);
                InformUser("Height Threshold Set: {0:F1}.", value);
            }
        }

        public double MeleeDistance
        {
            get { return Config.Instance.MeleeDistance; }
            set
            {
                SetProperty<double>(ref Config.Instance.MeleeDistance, value);
                InformUser("Melee Distance Set: {0:F1}.", value);
            }
        }

        public double RangedAttackDelay
        {
            get { return Config.Instance.RangedAttackDelay; }
            set
            {
                SetProperty<double>(ref Config.Instance.RangedAttackDelay, value);
                InformUser("Ranged Attack Distance Set: {0}.", value);
            }
        }

        public double WanderDistance
        {
            get { return Config.Instance.WanderDistance; }
            set
            {
                SetProperty<double>(ref Config.Instance.WanderDistance, (int)value);
                InformUser("Wander Distance Set: {0}.", (int)value);
            }
        }

        public int CastLatency
        {
            get { return Config.Instance.CastLatency; }
            set
            {
                SetProperty(ref Config.Instance.CastLatency, (int)value);
                InformUser("Cast Latency Set: {0}.", (int)value);
            }
        }

        public int GlobalCooldown
        {
            get { return Config.Instance.GlobalCooldown; }
            set
            {
                SetProperty(ref Config.Instance.GlobalCooldown, (int)value);
                InformUser("Global Cooldown Set: {0}.", (int)value);
            }
        }
    }
}

namespace EasyFarm.UserSettings
{
    /// <summary>
    /// Misc Settings
    /// </summary>
    public partial class Config
    {
        /// <summary>
        /// How far a player should detect a creature. 
        /// </summary>
        public double DetectionDistance = Constants.DETECTION_DISTANCE;

        /// <summary>
        /// How high or low a player should detect a creature. 
        /// </summary>
        public double HeightThreshold = Constants.HEIGHT_THRESHOLD;

        /// <summary>
        /// How close the player should be when attacking a creature. 
        /// </summary>
        public double MeleeDistance = Constants.MELEE_DISTANCE;

        /// <summary>
        /// The amount of time in seconds to wait before refiring a 
        /// ranged weapon. 
        /// </summary>
        public double RangedAttackDelay = 3;

        /// <summary>
        /// How far to go of the path for a unit. 
        /// </summary>
        public double WanderDistance = Constants.DETECTION_DISTANCE;

        /// <summary>
        /// Cast delay for laggy servers. 
        /// </summary>
        public int CastLatency = Constants.SPELL_CAST_LATENCY;

        /// <summary>
        /// Cast delay before casting next spell 
        /// (stops cannot use ability spam)
        /// </summary>
        public int GlobalCooldown = Constants.GLOBAL_SPELL_COOLDOWN;
    }    
}