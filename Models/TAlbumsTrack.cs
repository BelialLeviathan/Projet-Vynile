using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TAlbumsTrack
    {
        public int IdAlbum { get; set; }
        public int IdTracks { get; set; }
        public int TrackNumber { get; set; }

        public virtual TAlbum IdAlbumNavigation { get; set; } = null!;
        public virtual TTrack IdTracksNavigation { get; set; } = null!;
    }
}
