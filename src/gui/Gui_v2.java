package gui;

import control.ImportController;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

/**
 * Created by alt_mulig on 6/1/15.
 */
public class Gui_v2 {
    private JTabbedPane tabbedPane1;
    private JPanel panel1;
    private JButton importButton;
    private JButton optimizeButton;
    private JButton exportButton;
    private JScrollPane StandartRoutes;
    private JList status;

    public Gui_v2() {
        importButton.addActionListener(new ActionListener() {
        });
    }

    public void setData(ImportController data) {
    }

    public void getData(ImportController data) {
    }

    public boolean isModified(ImportController data) {
        return false;
    }

    @Override
    public void actionPerformed(ActionEvent actionEvent) {

    }

    private void createUIComponents() {
        // TODO: place custom component creation code here
    }
}
