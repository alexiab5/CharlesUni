package Printer;

public class LabelPrinter extends GenericPrinter {
    private String label;
    public LabelPrinter(String label){
        System.out.println("LabelPrinter is constructed");
        this.label = label;
    }
    @Override
    public void print(String message){
        System.out.println(label + " " + message);
    }
}
