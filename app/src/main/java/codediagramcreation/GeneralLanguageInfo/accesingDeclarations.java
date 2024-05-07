package codediagramcreation.GeneralLanguageInfo;

/**
 * pub - public
 * prv - private
 * pro - protected
 */
public enum accesingDeclarations {
    pub, prv, pro;

    public String toString(){
        if (this.equals(pub)) {
            return "+";
        }else if (this.equals(prv)) {
            return "-";
        } else if (this.equals(pro)) {
            return "#";
        }
        try {
            throw new InvalidEnumError("Invalid Enum for accesingDeclarations");
        } catch (InvalidEnumError e) {
            e.printStackTrace();
        }
        return null;
    }
}


class InvalidEnumError extends Throwable{
    public InvalidEnumError(String message){
        super(message);
    }
}