package gui;

import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;
import javax.swing.JButton;

import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

import javax.swing.JTable;
import javax.swing.JScrollPane;

import database.DBConnection;


public class Gui extends JFrame {

	private JPanel contentPane;
	private JTable table;

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
		setBounds(100, 100, 564, 312);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		// Import the default route
		JButton load = new JButton("Load");
		load.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				control.ImportController.getInstance().importRoutes();
	
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
		optimere.setBounds(221, 228, 89, 23);
		contentPane.add(optimere);
		
		//Export the new route
		JButton gem = new JButton("Gem");
		gem.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				control.ExportController.getInstance().exportDatas();
			}
		});
		gem.setBounds(449, 228, 89, 23);
		contentPane.add(gem);
		
		JScrollPane scrollPane = new JScrollPane();
		scrollPane.setBounds(10, 11, 528, 206);
		contentPane.add(scrollPane);
				
		table = new JTable();
		scrollPane.setViewportView(table);
		
	}
}

