using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.Domain.Tests
{
    public class ThreadTests
    {
        //public async void Foo() // Avoid "async void".

        public async Task Foo()
        {
            //var task = new Task(DoStuff);
            //task.Start();

            //var task = Task.Run(() => DoStuff());
            //await task;

            await Task.Run(() => DoStuff());
        }

        private void DoStuff()
        {
            
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public async Task NeedToInvokeFoo()
        {

        }
    }
}
