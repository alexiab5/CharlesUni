package cz.cuni.mff.bortosa.programs;
import cz.cuni.mff.bortosa.utils.BinarySearchTree;

public class Main {
    public static void main(String[] args) {
        BinarySearchTree bst = new BinarySearchTree();
        boolean inputError = false;

        if(args.length == 0){
            System.out.println("INPUT ERROR");
            System.exit(0);
        }

        for(String arg : args){
            try{
                int value = Integer.parseInt(arg);
                bst.add(value);
            }
            catch(Exception e){
                inputError = true;
                break;
            }
        }

        if(inputError){
            System.out.println("INPUT ERROR");
            System.exit(0);
        }

        for(Object val : bst){
            System.out.println(val);
        }
    }
}
