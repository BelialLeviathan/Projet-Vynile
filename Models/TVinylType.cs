using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TVinylType
    {
        public TVinylType()
        {
            TAlbumsVinylTypes = new HashSet<TAlbumsVinylType>();
        }

        public int IdVinylType { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<TAlbumsVinylType> TAlbumsVinylTypes { get; set; }
    }
}
