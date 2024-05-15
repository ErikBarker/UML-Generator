package codediagramcreation.languageScanners;

import java.lang.annotation.*;

@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.TYPE)
/**
 * Use this to list all of the finished and supported languages for the program
 */
public @interface Supported {

}
