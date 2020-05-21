import React, { useContext, useEffect } from 'react';
import { Form as FinalForm, Field } from 'react-final-form';
import { Form, Button, Header, Grid, Segment, Label } from 'semantic-ui-react';
import TextInput from '../../form/TextInput';
import { RootStoreContext } from '../../stores/rootStore';
import { FORM_ERROR } from 'final-form';
import { combineValidators, isRequired, composeValidators, isNumeric } from 'revalidate';
import ErrorMessage from '../../form/ErrorMessage';
import { INotice } from '../../models/notice';
import SelectInput from '../../form/SelectInput';
import DateInput from '../../form/DateInput';
import TextAreaInput from '../../form/TextAreaInput';
import agent from '../../api/agent';
import { history } from '../../index'
import { genderAddOptions, districtAddOptions } from '../../form/options/options';
import Login from '../User/Login';

const validate = combineValidators({
    name: isRequired({ message: 'Imię jest wymagane' }),
    surname: isRequired({ message: 'Nazwisko jest wymagane' }),
    age: composeValidators(
        isRequired({ message: 'Wiek jest wymagany' }),
        isNumeric({
            message: 'Wiek musi być liczbą'
        })
    )(),
    gender: isRequired({ message: 'Płeć jest wymagana' }),
    district: isRequired({ message: 'Województwo jest wymagane' }),
    city: isRequired({ message: 'Miasto jest wymagane' }),
    dateOfDisappearance: isRequired({ message: 'Data zaginięcia jest wymagana' }),

});

const AddNotice = () => {
    const rootStore = useContext(RootStoreContext);
    const { user } = rootStore.userStore;
    const { openModal } = rootStore.modalStore;

    useEffect(() => {
        if (user == null) {
            history.push('/');
            openModal(<Login />, null);
        }
    }, [openModal, user])

    const addNotice = async (values: INotice) => {
        try {
            const notice = await agent.Noticies.add(values)
                .then(response => {
                    history.push({
                        pathname: `/notice/details/${response}`,
                    });
                });
        } catch (error) {
            throw error;
        }
    }
    return (
        <FinalForm
            onSubmit={(values: INotice) => {
                values.age = Number(values.age);
                values.height = Number(values.height);
                addNotice(values)
                    .catch(error => ({
                        [FORM_ERROR]: error
                    }))
            }}
            validate={validate}
            render={({
                handleSubmit,
                submitting,
                submitError,
                invalid,
                pristine,
                dirtySinceLastSubmit
            }) => (
                    <Form onSubmit={handleSubmit} error style={{ marginBottom: '50px' }}>
                        <Header
                            as='h2'
                            content='Zgłoś osobę zaginioną'
                            color='black'
                            textAlign='center'
                        />
                        <p style={{ textAlign: 'justify' }}>
                            Natychmiast rozpocznij poszukiwania. Nie czekaj, aż sprawa „rozwiąże się sama”. Zgłoś zaginięcie policji i wypełnij poniższy formularz uzupełniając wszystkie pola.
                            Twoje ogłoszenie zostanie umieszczone w systemie i będzie dostępne dopóki nie zgłosisz jego usunięcia.
                        </p>
                        <Segment>
                            <Grid columns={2} relaxed='very' stackable>
                                <Grid.Column>
                                    <Segment padded>
                                        <Label attached='top'>Dane osoby zaginionej</Label>
                                        <Field name='name' component={TextInput} placeholder='Imię' />
                                        <Field name='surname' component={TextInput} placeholder='Nazwisko' />
                                        <Field
                                            name='gender'
                                            options={genderAddOptions}
                                            component={SelectInput}
                                            placeholder='Płeć'
                                        />
                                        <Field
                                            name='age'
                                            type="number"
                                            component={TextInput}
                                            placeholder='Wiek'
                                        />
                                        <Field
                                            name='height'
                                            type="number"
                                            component={TextInput}
                                            placeholder='Wzrost'
                                        />
                                        <Field
                                            name='specialCharacters'
                                            component={TextInput}
                                            placeholder='Znaki specjalne'
                                        />
                                        <Field
                                            name='eyeColor'
                                            component={TextInput}
                                            placeholder='Kolor oczu'
                                        />
                                    </Segment>
                                </Grid.Column>
                                <Grid.Column>
                                    <Segment>
                                        <Label attached='top'>Okoliczności zaginięcia</Label>
                                        <Field
                                            name='dateOfDisappearance'
                                            date={true}
                                            component={DateInput}
                                            placeholder='Data zaginięcia'
                                        />
                                        <Field
                                            name='city'
                                            component={TextInput}
                                            placeholder='Miasto'
                                        />
                                        <Field
                                            name='district'
                                            search={true}
                                            component={SelectInput}
                                            options={districtAddOptions}
                                            placeholder='Województwo'
                                        />
                                        <Field
                                            name='lastseenplace'
                                            component={TextInput}
                                            placeholder='Ostatnio widziany w'
                                        />
                                    </Segment>
                                </Grid.Column>
                            </Grid>
                            <Segment>
                                <Label attached='top'>Dodatkowe informacje</Label>
                                <Field
                                    name='description'
                                    rows={3}
                                    component={TextAreaInput}
                                    placeholder='Dodatkowe informacje'
                                />
                            </Segment>
                            {submitError && !dirtySinceLastSubmit && (
                                <ErrorMessage
                                    error={submitError}
                                />
                            )}
                        </Segment>
                        <Button
                            disabled={(invalid && !dirtySinceLastSubmit) || pristine}
                            loading={submitting}
                            content='Umieść ogłoszenie'
                            fluid
                            type='submit'
                        />
                    </Form>
                )}
        />
    );
};

export default AddNotice;