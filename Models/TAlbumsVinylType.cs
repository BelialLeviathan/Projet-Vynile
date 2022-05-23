using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TAlbumsVinylType
    {
        public int IdAlbum { get; set; }
        public int IdVinylType { get; set; }
        public string? Picture { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }

        public virtual TAlbum IdAlbumNavigation { get; set; } = null!;
        public virtual TVinylType IdVinylTypeNavigation { get; set; } = null!;
    }
}
