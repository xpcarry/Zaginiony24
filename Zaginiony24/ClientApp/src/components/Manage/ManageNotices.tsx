import React, { useState, useEffect } from 'react';
import agent from '../../api/agent';
import { Popup, Icon, Table } from 'semantic-ui-react';
import { IManageNotice } from '../../models/notice';
import LoadingComponent from '../../form/LoadingComponent';

const ManageNotices = () =>{

    const [notices, setNotices] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        setLoading(true);
        getNotices();
        setLoading(false);
    }, [])

    const getNotices = async () => {
        await agent.Noticies.listall()
            .then(response => {
                setNotices(response);
            })
            .catch(error => {
                console.log(error);
            })
    };

    const handleDelete = async (e:any, {value}:any) => {
        await agent.Noticies.deleteNotice(value)
            .then(response => {
            })
            getNotices();
    };

    if (loading) return <LoadingComponent content='Ładowanie...' />

    return(
        <Table verticalAlign='middle' compact celled >
        <Table.Header>
            <Table.Row>
                <Table.HeaderCell textAlign='center'>Imię i nazwisko<br/>osoby zaginionej</Table.HeaderCell>
                <Table.HeaderCell textAlign='center'>Dodane przez</Table.HeaderCell>
                <Table.HeaderCell textAlign='center'>Data dodania</Table.HeaderCell>
                <Table.HeaderCell textAlign='center'>Akcje</Table.HeaderCell>
            </Table.Row>
        </Table.Header>

        <Table.Body>
            {notices.map((notice: IManageNotice) => (
                <Table.Row key={notice.id}>
                    <Table.Cell textAlign='center'>{notice.name} {notice.surname}</Table.Cell>
                    <Table.Cell textAlign='center'>{notice.username}</Table.Cell>
                    <Table.Cell textAlign='center'>{notice.datePosted!.toString().split('T')[0]}</Table.Cell>
                    <Table.Cell textAlign='center'>                           
                        <Popup content='Usuń ogłoszenie' trigger={
                        <Icon value={notice.id} onClick={handleDelete} name='delete' style={{cursor:'pointer'}}/>
                        } />
                    </Table.Cell>
                </Table.Row>
            ))}
        </Table.Body>
    </Table>
    )
}
export default ManageNotices;