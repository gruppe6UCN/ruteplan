package gui;

import control.ImportController;

import javax.swing.*;
import java.awt.*;
import java.util.Vector;


public class Gui extends JFrame {

    private JPanel contentPane;
    private JTable table;
    private ImportController importController = ImportController.getInstance();

    /**
     * Launch the application.
     */
    public static void main(String[] args) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                try {
                    Gui frame = new Gui();
//                    frame.setVisible(true);
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });
    }

    /**
     * Create the frame.
     */
    public Gui() {
        Vector<String> columnNames = new Vector<String>();

        columnNames.addElement("Rute ID");
        columnNames.addElement("Default Rute ID");
        columnNames.addElement("Antal Stop");

        Vector<Vector> rowData = new Vector<Vector>();

        JTable table = new JTable(rowData, columnNames);
        JScrollPane scrollPane = new JScrollPane(table);


        JFrame frame = new JFrame("Table Printing");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        frame.add(scrollPane, BorderLayout.CENTER);
        scrollPane.setCorner(JScrollPane.UPPER_RIGHT_CORNER, new JButton("..."));


        frame.setSize(300, 150);
        frame.setVisible(true);


        // Prof
        importController.importRoutes(rowData);
    }
}