﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public class LogController
    {
          private static LogController ourInstance = new LogController();
    private DefaultListModel logReceiver = null;
    private int maxLogLength = 1000;

    public static LogController getInstance() {
        return ourInstance;
    }

    private LogController() {
    }

    public void setLogReceiver(DefaultListModel logReceiver) {
        this.logReceiver = logReceiver;
    }

    public void setMaxLogLength(int maxLogLength) {
        this.maxLogLength = maxLogLength;
    }


    synchronized public void StatusLog(String log) {
        try {
            if (logReceiver != null) {
                    logReceiver.add(0, log);
                    maintainLog();
            } else {
                System.out.println(log);
            }
        } catch (java.lang.ArrayIndexOutOfBoundsException e) {
        }
    }

    synchronized private void maintainLog() {
        if (logReceiver.size() > maxLogLength) {
            logReceiver.removeElement(logReceiver.lastElement());
        }
    }
    }
}