namespace TbNeo.Domain.Core
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        protected Entity() { }

        public Entity(int id)
        {
            Id = id;
        }
    }
}
