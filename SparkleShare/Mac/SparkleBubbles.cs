//   SparkleShare, a collaboration and sharing tool.
//   Copyright (C) 2010  Hylke Bons <hylkebons@gmail.com>
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program. If not, see <http://www.gnu.org/licenses/>.


using System;

using MonoMac.AppKit;
using MonoMac.Foundation;

namespace SparkleShare {
    
    public class SparkleBubbles : NSObject {

        public SparkleBubblesController Controller = new SparkleBubblesController ();


        public SparkleBubbles ()
        {
            Controller.ShowBubbleEvent += delegate (string title, string subtext, string image_path) {
                if (Environment.OSVersion.Version.Major < 12)
                    return; // The notification center was introduced in Mountain Lion

                InvokeOnMainThread (() => {
                    NSUserNotification notification = new NSUserNotification () {
                        Title           = title,
                        InformativeText = subtext,
                        DeliveryDate    = DateTime.Now
                    };
                    
                    NSUserNotificationCenter center  = NSUserNotificationCenter.DefaultUserNotificationCenter;
                    center.ShouldPresentNotification = delegate { return true; };
                    center.DidActivateNotification  += delegate { Controller.BubbleClicked (); };

                    center.ScheduleNotification (notification);
                });
            };
        }
    }
}
