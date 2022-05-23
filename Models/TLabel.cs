using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TLabel
    {
        public TLabel()
        {
            TAlbums = new HashSet<TAlbum>();
        }

        public int IdLabel { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<TAlbum> TAlbums { get; set; }
    }
}
