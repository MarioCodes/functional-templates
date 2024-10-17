package es.msanchez.templates.java.springboot.jpa.rest;

import com.googlecode.catchexception.apis.BDDCatchException;
import es.msanchez.templates.java.springboot.jpa.entity.Person;
import es.msanchez.templates.java.springboot.jpa.exception.DataTransferException;
import es.msanchez.templates.java.springboot.jpa.service.PersonService;
import org.assertj.core.api.BDDAssertions;
import org.mockito.BDDMockito;
import org.mockito.Mockito;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

public class PersonRestResourceTest {

    private PersonRestResource restResource;

    private PersonService serviceMock;

    @BeforeMethod
    public void setUp() {
        this.serviceMock = Mockito.mock(PersonService.class);

        this.restResource = new PersonRestResource(this.serviceMock);
    }

    @Test
    public void testCreatePerson() {
        // @GIVEN
        final String name = "mario";

        final boolean isValid = true;
        BDDMockito.given(this.serviceMock.isValid(name))
                .willReturn(isValid);

        // @WHEN
        this.restResource.createPerson(name);

        // @THEN
        BDDMockito.verify(this.serviceMock).isValid(name);
        BDDMockito.verify(this.serviceMock).save(Mockito.any(Person.class));
    }

    @Test
    public void testCreatePersonCaseNameNotValid() {
        // @GIVEN
        final String name = "mario";

        final boolean isValid = false;
        BDDMockito.given(this.serviceMock.isValid(name))
                .willReturn(isValid);

        // @WHEN
        BDDCatchException.when(this.restResource).createPerson(name);

        // @THEN
        BDDMockito.verify(this.serviceMock).isValid(name);
        BDDMockito.verify(this.serviceMock, Mockito.never()).save(Mockito.any(Person.class));
        BDDAssertions.assertThat(BDDCatchException.caughtException())
                .isInstanceOf(DataTransferException.class);
    }
}