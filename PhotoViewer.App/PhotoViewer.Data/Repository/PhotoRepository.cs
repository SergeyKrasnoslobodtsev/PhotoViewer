using Data.Model;

namespace Data.Repository
{
    public class PhotoRepository : BaseRepository<Photo>
    {

        public PhotoRepository(DataContext context) : base(context) {
        }
    }
}
