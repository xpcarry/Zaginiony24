import React, { useContext } from 'react';
import { Form as FinalForm, Field } from 'react-final-form';
import { Form, Button, Header } from 'semantic-ui-react';
import { RootStoreContext } from '../../stores/rootStore';
import { IUserFormValues } from '../../models/user';
import TextInput from '../../form/TextInput';
import { FORM_ERROR } from 'final-form';
import { combineValidators, isRequired } from 'revalidate';
import ErrorMessage from '../../form/ErrorMessage';
import Register from './Register';
import { Link } from 'react-router-dom';


const validate = combineValidators({
    email: isRequired({ message: 'Email jest wymagany' }),
    password: isRequired({ message: 'Hasło jest wymagane' })
});

const Login = () => {
    const rootStore = useContext(RootStoreContext);
    const { login } = rootStore.userStore;

    return (
        <FinalForm
            onSubmit={(values: IUserFormValues) => login(values).catch(error => ({
                [FORM_ERROR]: error
            }))}
            validate={validate}
            render={({ handleSubmit, submitting, submitError, invalid, pristine, dirtySinceLastSubmit }) => (
                <Form onSubmit={handleSubmit} error>
                    <Header
                        as='h2'
                        content='Zaloguj się do Zaginiony24'
                        color='black'
                        textAlign='center'
                    />
                    <Field 
                        name='email' 
                        component={TextInput} 
                        placeholder='Adres e-mail' 
                    />
                    <Field
                        name='password'
                        component={TextInput}
                        placeholder='Hasło'
                        type='password'
                    />
                    Nie masz konta?<Link to='#' style={{ color: 'black' }} onClick={() => { rootStore.modalStore.closeModal(); rootStore.modalStore.openModal(<Register />) }}> Zarejestruj się!</Link>
                    <Button 
                    style={{ marginTop: '10px' }} 
                    disabled={(invalid && !dirtySinceLastSubmit) || pristine} 
                    loading={submitting} 
                    fluid 
                    type='submit'>Zaloguj</Button>
                    {submitError && !dirtySinceLastSubmit &&
                        <ErrorMessage error={submitError} text='Nieprawidłowa nazwa użytkownika lub hasło' />}
                </Form>
            )}
        />
    );
};

export default Login;