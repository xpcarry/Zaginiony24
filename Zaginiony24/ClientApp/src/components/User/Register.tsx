import React, { useContext } from 'react';
import { Form as FinalForm, Field } from 'react-final-form';
import { Form, Button, Header } from 'semantic-ui-react';
import TextInput from '../../form/TextInput';
import { RootStoreContext } from '../../stores/rootStore';
import { IUserFormValues } from '../../models/user';
import { FORM_ERROR } from 'final-form';
import { combineValidators, isRequired, composeValidators, matchesField } from 'revalidate';
import ErrorMessage from '../../form/ErrorMessage';

const validate = combineValidators({
    username: isRequired('Username'),
    email: isRequired('Email'),
    password: isRequired({message:'Wpisz hasło'}),
    confirmPassword: composeValidators(
        isRequired({ message: 'Potwierdź hasło' }),
        matchesField('password', '')({message: 'Hasła się nie zgadzają'})
    )(),
});

const Register = () => {
    const rootStore = useContext(RootStoreContext);
    const { register } = rootStore.userStore;
    return (
        <FinalForm
            onSubmit={(values: IUserFormValues) =>
                register(values).catch(error => ({
                    [FORM_ERROR]: error
                }))
            }
            validate={validate}
            render={({
                handleSubmit,
                submitting,
                submitError,
                invalid,
                pristine,
                dirtySinceLastSubmit
            }) => (
                    <Form onSubmit={handleSubmit} error>
                        <Header
                            as='h2'
                            content='Zarejestruj się do Zaginiony24'
                            color='black'
                            textAlign='center'
                        />
                        <Field name='username' component={TextInput} placeholder='Nazwa użytkownika' />
                        <Field name='email' component={TextInput} placeholder='Email' />
                        <Field
                            name='name'
                            component={TextInput}
                            placeholder='Imię'
                        />
                        <Field
                            name='surname'
                            component={TextInput}
                            placeholder='Nazwisko'
                        />
                        <Field
                            name='password'
                            component={TextInput}
                            placeholder='Hasło'
                            type='password'
                        />
                        <Field
                            name='confirmPassword'
                            component={TextInput}
                            placeholder='Potwierdź hasło'
                            type='password'
                        />
                        {submitError && !dirtySinceLastSubmit && (
                            <ErrorMessage
                                error={submitError}
                            />
                        )}
                        <Button
                            disabled={(invalid && !dirtySinceLastSubmit) || pristine} 
                            loading={submitting}
                            fluid
                            type='submit'
                        >
                            Zarejestruj
                            </Button>
                    </Form>
                )}
        />
    );
};

export default Register;
