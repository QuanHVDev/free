using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace IronPirate {
    /// <summary>
    /// The helper class to excute a function from any where into Unity mainthread. Usefull for call back by Ads, Firebase, Facebook... or other third party.
    /// </summary>
    public class Excutor : SingletonBehaviourDontDestroy<Excutor> {
        #region private Queue Excute
        private readonly Queue<Action> tasks = new Queue<Action>();
        private readonly Queue<IEnumerator> enumerators = new Queue<IEnumerator>();

        private void Update() {
            this.ExcuteTasks();
        }

        private void ExcuteTasks() {
            if (tasks.Count > 0) {
                lock(tasks) {
                    Action task = tasks.Dequeue();
                    if (task != null) task.Invoke();
                }
            }

            if (enumerators.Count > 0) {
                lock(enumerators) {
                    IEnumerator ie = enumerators.Dequeue();
                    if (ie != null) {
                        Instance.StartCoroutine(ie);
                    }
                }
            }
        }

        private void Queue(Action task, float delayTime, bool waitInternet) {
            if (task == null) return;
            if (delayTime > 0) enumerators.Enqueue(IEDelay(task, delayTime, waitInternet));
            else if (!waitInternet) tasks.Enqueue(task);
            else enumerators.Enqueue(IEWaitInternet(task));
        }

        private void Queue(IEnumerator ie, float delayTime, bool waitInternet) {
            if (ie == null) return;
            if (delayTime > 0) enumerators.Enqueue(IEDelay(ie, delayTime, waitInternet));
            else if (!waitInternet) enumerators.Enqueue(ie);
            else enumerators.Enqueue(IEWaitInternet(ie));
        }

        private List<KeyValuePair<float, WaitForSecondsRealtime>> ieWaitForSecondCached = new List<KeyValuePair<float, WaitForSecondsRealtime>>(10);

        private WaitForSecondsRealtime Wait(float time) {
            var wait = ieWaitForSecondCached.Find((item) => item.Key == time);
            if (wait.Value != null) return wait.Value;
            else {
                var newWait = new WaitForSecondsRealtime(time);
                ieWaitForSecondCached.Add(new KeyValuePair<float, WaitForSecondsRealtime>(time, newWait));
                return newWait;
            }
        }

        private IEnumerator IEDelay(Action task, float delayTime, bool waitForInternet = false) {
            yield return Wait(delayTime);
            Queue(task, 0, waitForInternet);
        }
        private IEnumerator IEDelay(IEnumerator ie, float delayTime, bool waitForInternet = false) {
            yield return Wait(delayTime);
            Queue(ie, 0, waitForInternet);
        }

        private IEnumerator ieWaitInternet;
        private IEnumerator IEWaitInternet(Action task) {
            if (ieWaitInternet == null) {
                ieWaitInternet = new WaitUntil(() => HasInternet);
            }
            yield return ieWaitInternet;
            Queue(task, 0, false);
        }
        private IEnumerator IEWaitInternet(IEnumerator ie) {
            if (ieWaitInternet == null) {
                ieWaitInternet = new WaitUntil(() => HasInternet);
            }
            yield return ieWaitInternet;
            Queue(ie, 0, false);
        }

        private bool HasInternet => Application.internetReachability != NetworkReachability.NotReachable;
        #endregion

        ///<summary>The task will be add into a queue after 'delayTime' seconds, then be excute from main thread (on Update) </summary>
        public static void Schedule(Action task, float delayTime = 0, bool waitInternet = false) {
            if (task == null) return;
            Instance.Queue(task, delayTime, waitInternet);
        }

        ///<summary>The task will be add into a queue after 'delayTime' seconds, then be excute from main thread (on Update) </summary>
        public static void Schedule<T>(Action<T> task, T value, float delayTime = 0, bool waitInternet = false) {
            if (task == null) return;
            Instance.Queue(() => { task.Invoke(value); }, delayTime, waitInternet);
        }

        /**<summary>The enumerator will be add into a queue to be excute from main thread (on Update) </summary>*/
        public static void Schedule(IEnumerator enumerator, float delayTime = 0, bool waitInternet = false) {
            if (enumerator == null) return;
            Instance.Queue(enumerator, delayTime, waitInternet);
        }
    }
}