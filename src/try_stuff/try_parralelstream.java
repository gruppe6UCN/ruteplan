package try_stuff;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;

/**
 * Created by alt_mulig on 5/22/15.
 */


public class try_parralelstream {
    public static void main(String[] args) {
        //test1();
        test3();
    }

    private static void test1() {
        List<String> syncal1 = Collections.synchronizedList(new ArrayList<String>());
        List<String> syncal2 = Collections.synchronizedList(new ArrayList<String>());
//        List<String> syncal2 = new ArrayList<String>();
//        CopyOnWriteArrayList<String> syncal2 = new CopyOnWriteArrayList<String>();

        //Adding elements to synchronized ArrayList
        int count = 1000000;

        for (int i = 0; i < count; i++) {
            syncal1.add(String.valueOf(i));
        }

        syncal1.parallelStream().forEach((s -> syncal2.add(s)));

        try {
            BufferedWriter outputWriter = new BufferedWriter(new FileWriter("tester1212.txt"));
            for (int i = 0; i < syncal2.size(); i++) {
                // Maybe:
                outputWriter.write(syncal2.get(i) + "\n");
            }
            outputWriter.flush();
            outputWriter.close();
        } catch (IOException e) {
            e.printStackTrace();
        }

        synchronized (syncal2) {
            System.out.println("Iterating synchronized ArrayList:");
            Iterator<String> iterator = syncal2.iterator();
            while (iterator.hasNext())
                System.out.println(iterator.next());
        }
    }

    private static void test2() {
        List<String> ts = Collections.synchronizedList(new ArrayList<>());

        ts.add("1");
        ts.add("3");
        ts.add("3");
        ts.add("4");
        synchronized (ts) {
            ts.stream().forEach(t -> ts.remove(t));
        }
    }

    private static void test3() {
        ArrayList<String> ts = new ArrayList<>();

        ts.add("2");
        ts.add("1");
        ts.add("4");
        ts.add("3");

        Object[] array = ts.stream().sorted().toArray();
        String first = ts.stream().sorted().findFirst().get();

        System.out.println(first);
        for(int i = 0; i < array.length; i++) {
            System.out.println((String) array[i]);
        }
    }
}
