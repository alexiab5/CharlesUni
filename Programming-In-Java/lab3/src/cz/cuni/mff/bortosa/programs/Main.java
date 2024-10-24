package cz.cuni.mff.bortosa.programs;
import cz.cuni.mff.bortosa.util.GenericSimpleCollection;

public class Main {
    public static void main(String[] args) {
        GenericSimpleCollection collection = new GenericSimpleCollection();
//        for(String s : args){
//            int number = Integer.parseInt(s);
//            collection.add(number);
//        }
        for(String arg : args){
            collection.add(arg);
        }
        for(Object o : collection){
            System.out.println(o);
        }
    }
}