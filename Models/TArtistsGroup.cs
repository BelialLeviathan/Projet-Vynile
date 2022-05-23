using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TArtistsGroup
    {
        public int IdArtist { get; set; }
        public int IdGroup { get; set; }
        public bool? IsMember { get; set; }

        public virtual TArtist IdArtistNavigation { get; set; } = null!;
        public virtual TGroup IdGroupNavigation { get; set; } = null!;
    }
}
