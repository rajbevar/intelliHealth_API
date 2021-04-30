using System.Collections.Generic;

namespace PatientEngTranscription.Domain
{
    public abstract class Entity<T> where T : struct
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Entity{T}" /> class.
        /// </summary>
        protected Entity()
        {
        }
        /// <summary>
        /// Gets unique identifier for entity.
        /// </summary>
        public virtual T Id { get; set; }
        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<T>);
        }
        /// <summary>
        /// Indicates whether the current <see cref="Entity{T}"/> is equal to another one of the same type.
        /// </summary>
        /// <param name="obj">An <see cref="Entity{T}"/> to compare with this one.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Entity{T}" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Equals(Entity<T> obj)
        {
            if (obj == null)
            {
                return false;
            }
            /* due to lazy load usage obj or this types can be a subclass of real entity type, 
             * so obj and this can be equal, but have different types. */
            if (!this.GetType().IsInstanceOfType(obj) &&
                !obj.GetType().IsInstanceOfType(this))
            {
                return false;
            }
            if (EqualityComparer<T>.Default.Equals(this.Id, default(T)) && EqualityComparer<T>.Default.Equals(obj.Id, default(T)))
            {
                return ReferenceEquals(this, obj);
            }
            return EqualityComparer<T>.Default.Equals(this.Id, obj.Id);
        }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #region operators
        /// <summary>
        /// Returns a value indicating whether two <see cref="Entity{T}"/>s are equal.
        /// </summary>
        /// <param name="x">The first <see cref="Entity{T}"/> to compare.</param>
        /// <param name="y">The second <see cref="Entity{T}"/> to compare.</param>
        /// <returns>True if <paramref name="x"/> and <paramref name="y"/> are equal; otherwise, false.</returns>
        public static bool operator ==(Entity<T> x, Entity<T> y)
        {
            return object.Equals(x, y);
        }
        /// <summary>
        /// Returns a value indicating whether two <see cref="Entity{T}"/>s are not equal.
        /// </summary>
        /// <param name="x">The first <see cref="Entity{T}"/> to compare.</param>
        /// <param name="y">The second <see cref="Entity{T}"/> to compare.</param>
        /// <returns>True if <paramref name="x"/> and <paramref name="y"/> are not equal; otherwise, false.</returns>
        public static bool operator !=(Entity<T> x, Entity<T> y)
        {
            return !object.Equals(x, y);
        }
        #endregion
        /// <summary>
        /// Determines whether this instance is new.
        /// </summary>
        /// <returns></returns>
        public bool IsNew
        {
            get
            {
                return EqualityComparer<T>.Default.Equals(this.Id, default(T));
            }
        }
    }
}
