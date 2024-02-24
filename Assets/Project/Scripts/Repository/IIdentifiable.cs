public interface IIdentifiable<T> where T : Identificator
{
   public T Id{ get; set; }
}
