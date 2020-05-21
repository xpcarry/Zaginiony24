import React, { useState, useEffect, useContext, Fragment } from 'react';
import agent from '../../api/agent';
import { Table, Checkbox, Icon, Popup, Button } from 'semantic-ui-react';
import { IUserSettings } from '../../models/user';
import LoadingComponent from '../../form/LoadingComponent';
import { RootStoreContext } from '../../stores/rootStore';


const ManageUsers = () => {

    const rootStoreContext = useContext(RootStoreContext);
    const { openModal, closeModal } = rootStoreContext.modalStore;
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        setLoading(true);
        getUsers();
        setLoading(false);
    }, [])

    const getUsers = async () => {
        await agent.User.getuserslist()
            .then(response => {
                setUsers(response);
            })
            .catch(error => {
                console.log(error);
            })
    };

    const handleDelete = (e: any, { value }: any) => {
        openModal(
        <p>Wszystkie powiązane ogłoszenia zostaną usunięte, czy na pewno chcesz usunąć użytkownika? </p>,
            <Fragment>
                <Button
                    negative
                    onClick = {closeModal}
                >
                    Nie
                    </Button>
                <Button
                    positive
                    icon='checkmark'
                    labelPosition='right'
                    content='Tak'
                    value = {value}
                    onClick = {deleteUser}
                />
            </Fragment>)
    };

    const deleteUser = async (e: any, { value }:any) => {
        await agent.User.deleteUser(value)
        .then(response => {
        }).catch(error => {
            console.log(error);
        })
        closeModal();
        getUsers();
    }

    const handleLockoutUser = async (e: any, { value, checked }: any) => {
        const user = {} as IUserSettings;
        user.id = value;
        user.canLogIn = checked;
        await agent.User.changeLockoutStatus(user)
            .then(response => {
                checked = response;
            }).catch(error => {
                console.log(error);
            })
    }

    if (loading) return <LoadingComponent content='Ładowanie...' />

    return (
        <Table verticalAlign='middle' compact celled >
            <Table.Header>
                <Table.Row>
                    <Table.HeaderCell textAlign='center'>Nazwa użytkownika</Table.HeaderCell>
                    <Table.HeaderCell textAlign='center'>Adres e-mail</Table.HeaderCell>
                    <Table.HeaderCell textAlign='center'>Ogłoszenia</Table.HeaderCell>
                    <Table.HeaderCell textAlign='center'>Logowanie</Table.HeaderCell>
                    <Table.HeaderCell textAlign='center'>Akcje</Table.HeaderCell>
                </Table.Row>
            </Table.Header>

            <Table.Body>
                {users.map((user: IUserSettings) => (
                    <Table.Row key={user.id}>
                        <Table.Cell textAlign='center'>{user.username}</Table.Cell>
                        <Table.Cell textAlign='center'>{user.email}</Table.Cell>
                        <Table.Cell textAlign='center'>{user.noticesListedCount}</Table.Cell>
                        <Table.Cell textAlign='center'>
                            <Checkbox toggle value={user.id} defaultChecked={user.canLogIn} onChange={handleLockoutUser} />
                        </Table.Cell>
                        <Table.Cell textAlign='center'>
                            <Popup content='Usuń użytkownika' trigger={
                                <Icon value={user.id} onClick={handleDelete} name='user delete' style={{ cursor: 'pointer' }} />
                            } />
                        </Table.Cell>
                    </Table.Row>
                ))}
            </Table.Body>
        </Table>
    )
}
export default ManageUsers;