package gui;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;


public class Gui extends JFrame {

    private JPanel contentPane;

    /**
     * Launch the application.
     */
    public static void main(String[] args) {
        EventQueue.invokeLater(new Runnable() {
            public void run() {
                try {
                    Gui frame = new Gui();
                    frame.setVisible(true);
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
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setBounds(100, 100, 450, 300);
        contentPane = new JPanel();
        //contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
        setContentPane(contentPane);
        contentPane.setLayout(null);



        DefaultTableModel model  = new DefaultTableModel();
        model.addColumn("Rute ID");
        model.addColumn("Default Rute ID");
        model.addColumn("Antal Stop");

        JTable table = new JTable(model);
        table.setBounds(10, 11, 414, 209);
        contentPane.add(table);

        // Import the default route
        JButton load = new JButton("Load");
        load.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent arg0) {
            }
        });
        load.setBounds(10, 228, 89, 23);
        contentPane.add(load);

        //Optimise the route
        JButton optimere = new JButton("Optimere");
        optimere.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent arg0) {
                control.OptimizeController.getInstance().optimize();

            }
        });
        optimere.setBounds(168, 228, 89, 23);
        contentPane.add(optimere);

        //Export the new route
        JButton gem = new JButton("Gem");
        gem.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent arg0) {
//                control.ExportController.getInstance().exportDatas();
                table.updateUI();
            }
        });
        gem.setBounds(335, 228, 89, 23);
        contentPane.add(gem);

        JScrollPane scrollPane = new JScrollPane(table);
        scrollPane.setBackground(Color.WHITE);
        scrollPane.setBounds(10, 10, 414, 210);
        scrollPane.setCorner(JScrollPane.UPPER_RIGHT_CORNER, new JButton("..."));
        contentPane.add(scrollPane);
    }
}

