namespace SearchLib
{
    public interface IFrontier
    {
        void Add(Node node);
        Node Remove();
        void Clear();
        bool IsEmpty();
        bool Contains(Node node);
    }
}
