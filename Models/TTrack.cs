using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TTrack
    {
        public TTrack()
        {
            TAlbumsTracks = new HashSet<TAlbumsTrack>();
            IdGenres = new HashSet<TGenre>();
        }

        public int IdTracks { get; set; }
        public string TrackName { get; set; } = null!;
        public int? DurationSec { get; set; }

        public virtual ICollection<TAlbumsTrack> TAlbumsTracks { get; set; }

        public virtual ICollection<TGenre> IdGenres { get; set; }
    }
}
