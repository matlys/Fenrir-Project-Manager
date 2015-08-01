namespace Model.Enums
{
    /// <summary>
    /// Represents current project status
    /// </summary>
    public enum ProjectStatus
    {
        Open = 1,
        Paused,
        Closed
    }

    /// <summary>
    /// Represents current issue status
    /// </summary>
    public enum IssueStatus
    { 
        New = 1,
        InProgress,
        Finished,
        Closed,
        Checking,
        ReOpen
    }

    /// <summary>
    /// Represents issue type
    /// </summary>
    public enum IssueType
    {
        Feature = 1,
        Bug,
        Test
    }

    /// <summary>
    /// Represents current issue progress
    /// </summary>
    public enum IssueProgress
    {
        OnStart = 0,
        SomethingIsDone = 25,
        DoneInHalf= 50,
        AlmostDone= 75,
        Done = 100
    }

    /// <summary>
    /// Represents possible roles of user
    /// </summary>
    public enum UserRole
    {
        Administrator = 1,
        ProjectManager,
        Developer,
        Observer
    }
}
