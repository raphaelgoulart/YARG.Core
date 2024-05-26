using System;
using System.Collections.Generic;

namespace YARG.Core.Audio
{
    public static class AudioHelpers
    {
        public const double SONG_VOLUME_MULTIPLIER = 0.7;
        public static readonly Dictionary<string, SongStem> SupportedStems = new()
        {
            { "song",     SongStem.Song    },
            { "guitar",   SongStem.Guitar  },
            { "bass",     SongStem.Bass    },
            { "rhythm",   SongStem.Rhythm  },
            { "keys",     SongStem.Keys    },
            { "vocals",   SongStem.Vocals  },
            { "vocals_1", SongStem.Vocals1 },
            { "vocals_2", SongStem.Vocals2 },
            { "drums",    SongStem.Drums   },
            { "drums_1",  SongStem.Drums1  },
            { "drums_2",  SongStem.Drums2  },
            { "drums_3",  SongStem.Drums3  },
            { "drums_4",  SongStem.Drums4  },
            { "crowd",    SongStem.Crowd   },
            // "preview"
        };

        public static readonly IList<string> SfxPaths = new[]
        {
            "note_miss",
            "starpower_award",
            "starpower_gain",
            "starpower_deploy",
            "starpower_release",
            "clap",
            "star",
            "star_gold",
            "overstrum_1",
            "overstrum_2",
            "overstrum_3",
            "overstrum_4",
            "piano_0",
            "piano_1",
            "piano_2",
            "piano_3",
            "piano_4",
            "piano_5",
            "piano_6",
            "piano_7",
            "piano_8",
            "piano_9",
            "piano_10",
            "piano_11",
            "piano_12",
            "piano_13",
            "piano_14",
            "piano_15",
            "piano_16",
            "piano_17",
            "piano_18",
            "piano_19",
            "piano_20",
            "piano_21",
            "piano_22",
            "piano_23",
            "piano_24",
        };

        public static readonly IList<double> SfxVolume = new[]
        {
            0.55,
            0.5,
            0.5,
            0.4,
            0.5,
            0.16,
            1.0,
            1.0,
            0.4,
            0.4,
            0.4,
            0.4,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
        };

        public static readonly List<SongStem> PitchBendAllowedStems = new()
        {
            SongStem.Guitar,
            SongStem.Bass,
            SongStem.Rhythm,
        };

        public static SongStem GetStemFromName(string stem)
        {
            return stem.ToLowerInvariant() switch
            {
                "song"       => SongStem.Song,
                "guitar"     => SongStem.Guitar,
                "bass"       => SongStem.Bass,
                "rhythm"     => SongStem.Rhythm,
                "keys"       => SongStem.Keys,
                "vocals"     => SongStem.Vocals,
                "vocals_1"   => SongStem.Vocals1,
                "vocals_2"   => SongStem.Vocals2,
                "drums"      => SongStem.Drums,
                "drums_1"    => SongStem.Drums1,
                "drums_2"    => SongStem.Drums2,
                "drums_3"    => SongStem.Drums3,
                "drums_4"    => SongStem.Drums4,
                "crowd"      => SongStem.Crowd,
                // "preview" => SongStem.Preview,
                _ => SongStem.Song,
            };
        }

        public static SongStem ToSongStem(this Instrument instrument)
        {
            return instrument switch
            {
                Instrument.FiveFretGuitar or
                Instrument.SixFretGuitar or
                Instrument.ProGuitar_17Fret or
                Instrument.ProGuitar_22Fret => SongStem.Guitar,

                Instrument.FiveFretBass or
                Instrument.SixFretBass or
                Instrument.ProBass_17Fret or
                Instrument.ProBass_22Fret => SongStem.Bass,

                Instrument.FiveFretRhythm or
                Instrument.SixFretRhythm or
                Instrument.FiveFretCoopGuitar or
                Instrument.SixFretCoopGuitar => SongStem.Rhythm,

                Instrument.Keys or
                Instrument.ProKeys => SongStem.Keys,

                Instrument.ProDrums or
                Instrument.FourLaneDrums or
                Instrument.FiveLaneDrums => SongStem.Drums,

                Instrument.Vocals or
                Instrument.Harmony => SongStem.Vocals,

                _ => throw new Exception("Unreachable.")
            };
        }

        public static SfxSample GetSfxFromName(string sfx)
        {
            return sfx.ToLowerInvariant() switch
            {
                "note_miss"         => SfxSample.NoteMiss,
                "starpower_award"   => SfxSample.StarPowerAward,
                "starpower_gain"    => SfxSample.StarPowerGain,
                "starpower_deploy"  => SfxSample.StarPowerDeploy,
                "starpower_release" => SfxSample.StarPowerRelease,
                "clap"              => SfxSample.Clap,
                "star"              => SfxSample.StarGain,
                "star_gold"         => SfxSample.StarGold,
                "overstrum_1"       => SfxSample.Overstrum1,
                "overstrum_2"       => SfxSample.Overstrum2,
                "overstrum_3"       => SfxSample.Overstrum3,
                "overstrum_4"       => SfxSample.Overstrum4,
                "piano_0"           => SfxSample.Piano_0,
                "piano_1"           => SfxSample.Piano_1,
                "piano_2"           => SfxSample.Piano_2,
                "piano_3"           => SfxSample.Piano_3,
                "piano_4"           => SfxSample.Piano_4,
                "piano_5"           => SfxSample.Piano_5,
                "piano_6"           => SfxSample.Piano_6,
                "piano_7"           => SfxSample.Piano_7,
                "piano_8"           => SfxSample.Piano_8,
                "piano_9"           => SfxSample.Piano_9,
                "piano_10"          => SfxSample.Piano_10,
                "piano_11"          => SfxSample.Piano_11,
                "piano_12"          => SfxSample.Piano_12,
                "piano_13"          => SfxSample.Piano_13,
                "piano_14"          => SfxSample.Piano_14,
                "piano_15"          => SfxSample.Piano_15,
                "piano_16"          => SfxSample.Piano_16,
                "piano_17"          => SfxSample.Piano_17,
                "piano_18"          => SfxSample.Piano_18,
                "piano_19"          => SfxSample.Piano_19,
                "piano_20"          => SfxSample.Piano_20,
                "piano_21"          => SfxSample.Piano_21,
                "piano_22"          => SfxSample.Piano_22,
                "piano_23"          => SfxSample.Piano_23,
                "piano_24"          => SfxSample.Piano_24,
                _                   => SfxSample.NoteMiss,
            };
        }
    }
}