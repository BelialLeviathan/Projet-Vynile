using System;
using System.Collections.Generic;

namespace projet_boutique_vinyle.Models
{
    public partial class TGenre
    {
        public TGenre()
        {
            IdTracks = new HashSet<TTrack>();
        }

        public int IdGenre { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<TTrack> IdTracks { get; set; }
    }
}
