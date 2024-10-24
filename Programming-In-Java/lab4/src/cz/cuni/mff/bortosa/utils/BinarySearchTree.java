package cz.cuni.mff.bortosa.utils;
import java.util.Iterator;
import java.util.Stack;

class Node {
    int value;
    Node leftNode;
    Node rightNode;

    public Node(int val){
        value = val;
        leftNode = null;
        rightNode = null;
    }
}

public class BinarySearchTree implements Iterable {
    private Node root;

    public BinarySearchTree(){
        root = null;
    }

    public void add(int value){
        Node newNode = new Node(value);
        Node currentNode = root;
        Node previousNode = null;

        while(currentNode != null) {
            previousNode = currentNode;
            if(newNode.value < currentNode.value) {
                currentNode = currentNode.leftNode;
            }
            else{
                currentNode = currentNode.rightNode;
            }
        }
        if(previousNode == null) {
            root = newNode;
            return;
        }
        if(previousNode.value < newNode.value) {
            previousNode.rightNode = newNode;
        }
        else{
            previousNode.leftNode = newNode;
        }
    }

    public Iterator iterator() {
        return new BSTIterator();
    }

    private class BSTIterator implements Iterator {
        private Stack<Node> nextNodes;

        BSTIterator() {
            nextNodes = new Stack<Node>();
            pushLeft(root);
        }

        void pushLeft(Node node){
            while (node != null) {
                nextNodes.push(node);
                node = node.leftNode;
            }
        }

        public boolean hasNext(){
            return !nextNodes.isEmpty();
        }

        public Object next() {
            Node currentNode = nextNodes.pop();
            int value = currentNode.value;
            if(currentNode != null) {
                pushLeft(currentNode.rightNode);
            }
            return value;
        }
    }
}
