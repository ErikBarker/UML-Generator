/*
 * This Java source file was generated by the Gradle 'init' task.
 */
package codediagramcreation;

import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

import java.io.IOException;

class AppTest {
    @Test void appHasAGreeting() {
        App classUnderTest = new App();
        assertNotNull(classUnderTest.getGreeting(), "app should have a greeting");
    }

    @Test void nullDirectory(){
        App classUnderTest = new App();
        assertThrows(NullPointerException.class, () -> classUnderTest.main(new String[]{null}), "App should throw Error on null directory");
    }

    @Test void invalidDirectory(){
        App classUnderTest = new App();
        assertThrows(IOException.class, () -> classUnderTest.main(new String[]{"invalidDir"}), "App should throw Error on invalid directory");
    }

    @Test void emptyDirectory(){
        App classUnderTest = new App();
        assertThrows(Exception.class, () -> classUnderTest.main(new String[]{".\\app\\test\\resources\\emptyDir"}), "App should throw Error on empty directory");
    }
}
