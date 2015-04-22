// Copyright 2008 Blue Onion Software
// All rights reserved

namespace BlueOnion
{
    using System.Drawing;
    using System.Windows.Forms;

    static class Effects
    {
        static BigMansStuff.LocusEffects.LocusEffectsProvider _locusEffectsProvider;

        static void Initialize()
        {
            if (_locusEffectsProvider != null)
                return;

            _locusEffectsProvider = new BigMansStuff.LocusEffects.LocusEffectsProvider();
            _locusEffectsProvider.Initialize();

            var heartbeatEffect = new BigMansStuff.LocusEffects.BeaconLocusEffect
                                      {
                                          Name = "HeartBeatBeacon",
                                          InitialSize = new Size(100, 100),
                                          AnimationTime = 4000,
                                          LeadInTime = 0,
                                          LeadOutTime = 0,
                                          AnimationStartColor = Color.HotPink,
                                          AnimationEndColor = Color.HotPink,
                                          AnimationOuterColor = Color.Pink,
                                          RingWidth = 4,
                                          OuterRingWidth = 4,
                                          BodyFadeOut = true,
                                          ShowShadow = true,
                                          Style = BigMansStuff.LocusEffects.BeaconEffectStyles.HeartBeat
                                      };

            _locusEffectsProvider.AddLocusEffect(heartbeatEffect);
        }

        public static void ShowEffect(Form form, Point location)
        {
            Initialize();
            var iconSize = SystemInformation.IconSize;
            var center = new Point {X = location.X - iconSize.Width, Y = location.Y - (iconSize.Height / 2)};
            var screenBounds = new Rectangle(center, new Size(100, 100));
            _locusEffectsProvider.ShowLocusEffect(form, screenBounds, "HeartBeatBeacon");
        }
    }
}
