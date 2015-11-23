using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Enums
{

    // should be deleted in next commit
    public struct StaticData
    { 
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public byte[] UserAvatar { get; set; }
        public byte[] ProjectLogo { get; set; }
        public string ProjectName { get; set; }
    }   

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
        [Display(Name = "No progress, sorry")]
        OnStart = 0,

        [Display(Name = "I do something")]
        SomethingIsDone = 25,

        [Display(Name = "I think I'm in half")]
        DoneInHalf = 50,

        [Display(Name = "Almost done")]
        AlmostDone = 75,

        [Display(Name = "I'm finish")]
        Done = 100
    }

    /// <summary>
    /// Represents possible roles of user
    /// </summary>
    public enum UserRole
    {
        [Display(Name = "Administrator")]
        Administrator = 1,

        [Display(Name = "Project manager")]
        ProjectManager,

        [Display(Name = "Developer")]
        Developer,

        [Display(Name = "Observer")]
        Observer
    }
}
