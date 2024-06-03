namespace DH_GUIPresenters.Visitor
{
    public interface Visitable
    {
        void AcceptVisitFrom(Visitor v);
    }
}
