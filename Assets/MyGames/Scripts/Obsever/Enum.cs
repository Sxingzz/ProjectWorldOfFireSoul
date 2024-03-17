public enum ListenType
{
    ANY = 0,
    LEFT_MOUSE_CLICK,
    RIGHT_MOUSE_CLICK,
    UPDATE_PLAYER_INFO,
    RELOAD_ANIMATION_EVENT,
    UPDATE_AMMO,
    UPDATE_HEALTH,
    UPDATE_ENEMY_COUNT,
    UPDATE_QUEST_TEXT,
    UPDATE_TASK_COMPLETE,
    UPDATE_RECEIVE_TASK,

}

public enum UIType
{
    UNKNOWN,
    SCREEN,
    POPUP,
    NOTIFY,
    OVERLAP
}

public enum WeaponSlot
{
    Primary = 0,
    Secondary = 1,
    Third = 2,
}

public enum EnemyStateID
{
    ChasePlayer,
    Death,
    Idle,
    FindWeapon,
    AttackPlayer,
    Patrol,
}

public enum SocketID
{
    Spine,
    RightHand
}

public enum EquipWeaponBy
{
    Enemy,
    Player
}

public enum QuestStatus
{
    NotStarted,
    InProgress,
    Completed
}
