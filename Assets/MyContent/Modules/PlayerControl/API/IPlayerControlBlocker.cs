namespace Modules.PlayerControl.API
{
    public interface IPlayerControlBlocker
    {
        void BlockControl (object requester);
        void ReleaseControl(object requester);
    }
}