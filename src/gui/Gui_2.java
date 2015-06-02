package gui;

import control.ExportController;
import control.ImportController;
import control.LogController;
import control.OptimizeController;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.Vector;

public class Gui_2 {

	private JFrame frame;
	private JSplitPane splitPane;
	private JTable defaultTable;
	private JTable optimizedTable;
	private JButton importButton;
	private JButton optimizeButton;
	private JButton exportButton;

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					Gui_2 window = new Gui_2();
					window.frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the application.
	 */
	public Gui_2() {
		initialize();
	}

    private void setButtonsToEnabled(boolean enabled) {
        importButton.setEnabled(enabled);
        optimizeButton.setEnabled(enabled);
        exportButton.setEnabled(enabled);
    }

	/**
	 * Initialize the contents of the frame.
	 */
	private void initialize() {
		frame = new JFrame();
		BorderLayout borderLayout = (BorderLayout) frame.getContentPane().getLayout();
		borderLayout.setVgap(200);
		borderLayout.setHgap(200);
		frame.setBounds(100, 100, 403, 335);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		splitPane = new JSplitPane();
		splitPane.setResizeWeight(0.8);
		splitPane.setOneTouchExpandable(true);
		splitPane.setOrientation(JSplitPane.VERTICAL_SPLIT);
		frame.getContentPane().add(splitPane, BorderLayout.CENTER);

		JPanel tabPanel = new JPanel();
		splitPane.setLeftComponent(tabPanel);
		tabPanel.setLayout(new BoxLayout(tabPanel, BoxLayout.Y_AXIS));

		JTabbedPane tabbedPane = new JTabbedPane(JTabbedPane.TOP);
		tabPanel.add(tabbedPane);

		JScrollPane defaultRoutes = new JScrollPane();
		tabbedPane.addTab("Default Routes", null, defaultRoutes, null);

		defaultTable = new JTable();
		defaultRoutes.setViewportView(defaultTable);

		JScrollPane optimizedRoutes = new JScrollPane();
		tabbedPane.addTab("Optimize", null, optimizedRoutes, null);

		optimizedTable = new JTable();
		optimizedRoutes.setViewportView(optimizedTable);

		JPanel panel_2 = new JPanel();
		tabPanel.add(panel_2);
		panel_2.setLayout(new BoxLayout(panel_2, BoxLayout.X_AXIS));

		Component horizontalGlue_2 = Box.createHorizontalGlue();
		panel_2.add(horizontalGlue_2);

		importButton = new JButton("Import");
		importButton.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                new Thread(new Runnable() {
                    public void run() {
                        setButtonsToEnabled(false);
                        tabbedPane.setSelectedComponent(defaultRoutes);
                        DefaultTableModel model = new DefaultTableModel();

                        defaultTable.setModel(model);

                        model.addColumn("Route ID");
                        model.addColumn("No Stops");
                        model.addColumn("Load/Capacity");

                        Vector rows = model.getDataVector();
                        rows.removeAllElements();
                        ImportController.getInstance().importRoutes(rows);
                        model.fireTableDataChanged();

                        setButtonsToEnabled(true);
                    }
                }).start();
            }
        });
		importButton.setHorizontalAlignment(SwingConstants.LEFT);
		panel_2.add(importButton);

		Component horizontalGlue = Box.createHorizontalGlue();
		panel_2.add(horizontalGlue);

		optimizeButton = new JButton("Optimize");
		optimizeButton.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                new Thread(new Runnable() {
                    public void run() {
                        setButtonsToEnabled(false);
                        tabbedPane.setSelectedComponent(optimizedRoutes);
                        DefaultTableModel model = new DefaultTableModel();

                        optimizedTable.setModel(model);

                        model.addColumn("Default Route ID");
                        model.addColumn("No Stops");
                        model.addColumn("Load/Capacity");
                        model.addColumn("Time for departure");
                        model.addColumn("Extra Route");

                        Vector rows = model.getDataVector();
                        rows.removeAllElements();
                        OptimizeController.getInstance().optimize(rows);
                        model.fireTableDataChanged();

                        setButtonsToEnabled(true);
                    }
                }).start();
            }
        });
		panel_2.add(optimizeButton);

		Component horizontalGlue_1 = Box.createHorizontalGlue();
		panel_2.add(horizontalGlue_1);

		exportButton = new JButton("Export");
		exportButton.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                new Thread(new Runnable() {
                    public void run() {
                        setButtonsToEnabled(false);
                        tabbedPane.setSelectedComponent(optimizedRoutes);
                        DefaultTableModel model = new DefaultTableModel();

                        optimizedTable.setModel(model);

                        model.addColumn("Route ID");
                        model.addColumn("Default Route ID");
                        model.addColumn("No Stops");
                        model.addColumn("Load/Capacity");
                        model.addColumn("Time for departure");
                        model.addColumn("Extra Route");

                        Vector rows = model.getDataVector();
                        rows.removeAllElements();
                        ExportController.getInstance().exportDatas(rows);
                        model.fireTableDataChanged();

                        setButtonsToEnabled(true);
                    }
                }).start();
            }
        });
		exportButton.setHorizontalAlignment(SwingConstants.RIGHT);
		panel_2.add(exportButton);

		Component horizontalGlue_3 = Box.createHorizontalGlue();
		panel_2.add(horizontalGlue_3);

		JPanel statusBarPanel = new JPanel();
		splitPane.setRightComponent(statusBarPanel);
		statusBarPanel.setLayout(new BorderLayout(0, 0));

        JList statusBar = new JList();
        statusBar.setModel(new DefaultListModel());
        LogController.getInstance().setLogReceiver((DefaultListModel) statusBar.getModel());
		statusBarPanel.add(statusBar);
	}
	public JSplitPane getSplitPane() {
		return splitPane;
	}
	protected JFrame getFrame() {
		return frame;
	}
}
