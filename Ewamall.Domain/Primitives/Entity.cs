using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewamall.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Entity()
        {
            
        }
        protected Entity(int id) {
            Id = id;
        }
        [Key]
        public int Id { get; private init; }
/*        public static bool operator ==(Entity? left, Entity? right)
        {
            return left is not null && right is not null && left.Equals(right);
        }*/
/*        public static bool operator !=(Entity? left, Entity? right)
        {
            return !(left is not null && right is not null && left.Equals(right));
        }*/
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (!(obj is Entity entity))
                return false;

            if(obj.GetType() != GetType())
            {
                return false;
            }

            return entity.Id == Id;
        }

        public bool Equals(Entity? other)
        {
            if(other is null)
            {
                return false;
            }
            if(other.GetType() != GetType())
            {
                return false;
            }
            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() * 41;
        }
    }
}
