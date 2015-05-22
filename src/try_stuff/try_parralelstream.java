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
}
