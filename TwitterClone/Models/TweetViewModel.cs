using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace TwitterClone.Models
{
    public class TweetModel
    {
        public int TweetId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public string CreatedDate { get; set; }
    }

    public class FollowingModel
    {
        public string UserId { get; set; }
        public string FollowingId { get; set; }
    }

    public class TweetViewModel
    {
        public Collection<TweetModel> Tweets { get; set; }
        public Collection<FollowingModel> Followings { get; set; }

        public Collection<TweetModel> MyTweets { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public int MyTweetsCount { get; set; }
        public bool IsTweetPage { get; set; }
    }

}