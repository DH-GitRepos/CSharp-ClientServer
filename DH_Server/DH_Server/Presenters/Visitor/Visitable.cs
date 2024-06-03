namespace DH_Server.Presenters.Visitor
{
    interface Visitable
    {
        void AcceptVisitFrom(Visitor v);
    }
}
