using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace yt_DesignUI
{
    public static class Animator
    {
        public static List<Animation> AnimationList = new List<Animation>();

        public static int Count()
        {
            return AnimationList.Count;
        }

        private static Thread AnimatorThread;

        private static double Interval;

        public static bool IsWork = false;
        
        public static void Start()
        {
            IsWork = true;
            Interval = 15; // FPS ~65

            AnimatorThread = new Thread(AnimationInvoker)
            {
                IsBackground = true,
                Name = "UI Animation"
            };

            AnimatorThread.Start();
        }

        private static void AnimationInvoker()
        {
            while (true)
            {
                AnimationList.RemoveAll(a => a.Status == Animation.AnimationStatus.Completed);

                Parallel.For(0, Count(), index =>
                {
                    AnimationList[index].UpdateFrame();
                });

                Thread.Sleep((int)Interval);
            }
        }

        public static void Request(Animation Anim, bool ReplaceIfExists)
        {
            Anim.Status = Animation.AnimationStatus.Requested;

            Animation dupAnim = GetDuplicate(Anim);

            if(dupAnim != null)
            {
                if(ReplaceIfExists == true)
                {
                    dupAnim.Status = Animation.AnimationStatus.Completed;
                }
                else
                {
                    return;
                }
            }

            AnimationList.Add(Anim);
        }

        private static Animation GetDuplicate(Animation Anim)
        {
            return AnimationList.Find(a => a.ID == Anim.ID);
        }
    }
}
