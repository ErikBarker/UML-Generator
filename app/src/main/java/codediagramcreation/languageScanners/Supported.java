package codediagramcreation.languageScanners;

import java.lang.annotation.*;

@Retention(RetentionPolicy.RUNTIME)
@Target({ElementType.TYPE, ElementType.METHOD})
/**
 * Use this to list all of the finished and supported langues for the program
 */
public @interface Supported {

}
