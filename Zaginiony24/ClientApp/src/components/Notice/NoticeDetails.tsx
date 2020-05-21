import React, { useState, useEffect, Fragment } from 'react';
import LoadingComponent from '../../form/LoadingComponent';
import agent from '../../api/agent';
import { Grid, Image, Header, Table, Segment } from 'semantic-ui-react';
import { IDetails } from '../../models/details';

const NoticeDetails = ({
  match,
}: any, ) => {
  const [notice, setNotice] = useState<IDetails>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadNotice(match.params.id);
  }, [match.params.id])

  const loadNotice = async (id: string) => {
    try {
      await agent.Noticies.details(id)
        .then((response: IDetails) => {
          setNotice(response);
        });
      setLoading(false);
    } catch (error) {
      console.log(error);
      setLoading(false);
    }

  }

  if (loading) return <LoadingComponent content='Ładowanie apikacji...' />;
  if (!notice) return <h2>Nie odnaleziono ogłoszenia</h2>;

  return (
    <Fragment>
      <Header style={{ textAlign: 'center' }} as='h2'>Zaginiony: {notice.notice.name} {notice.notice.surname}</Header>
      <Grid stackable>
        <Grid.Column width={4}>
          <Image src='https://react.semantic-ui.com/images/wireframe/image.png' />
        </Grid.Column>
        <Grid.Column width={8}>
          <Table basic>
            <Table.Body>
              <Table.Row>
                <Table.Cell>Wiek:</Table.Cell>
                <Table.Cell>{notice.notice.age} lat</Table.Cell>
              </Table.Row>
              <Table.Row>
                <Table.Cell>Data zaginięcia:</Table.Cell>
                <Table.Cell>{notice.notice.dateOfDisappearance!.toString().split('T')[0]}</Table.Cell>
              </Table.Row>
              <Table.Row>
                <Table.Cell>Wzrost:</Table.Cell>
                <Table.Cell>{notice.notice.height} cm</Table.Cell>
              </Table.Row>
              <Table.Row>
                <Table.Cell>Kolor oczu:</Table.Cell>
                <Table.Cell>{notice.notice.eyeColor}</Table.Cell>
              </Table.Row>
              <Table.Row>
                <Table.Cell>Znaki szczególne:</Table.Cell>
                <Table.Cell>{notice.notice.specialCharacters ?? 'brak'}</Table.Cell>
              </Table.Row>
              <Table.Row>
                <Table.Cell>Ostatnie miejsce zamieszkania:</Table.Cell>
                <Table.Cell>{notice.notice.city}, {notice.notice.district}</Table.Cell>
              </Table.Row>
              <Table.Row>
                <Table.Cell>Ostatnie widziany w:</Table.Cell>
                <Table.Cell>{notice.notice.lastSeenPlace}</Table.Cell>
              </Table.Row>
            </Table.Body>
          </Table>
          <Header as='h3'>Dodatkowe informacje:</Header>
          <Segment>
            {notice.notice.description}
          </Segment>
        </Grid.Column>
        <Grid.Column width={4}>
          <Segment padded style={{textAlign:'center'}}>
            <b>Ogłoszenie zamieszczone</b> <br/> {notice.notice.datePosted!.toString().split('T')[0]}
          </Segment>
          <Segment padded style={{textAlign:'center'}}>
            <b>Dane użytkownika</b><br/>
            {notice.user.name}<br/>
            {notice.user.username}<br/>
            <a href={`mailto:${notice.user.email}`}>{notice.user.email}</a>
          </Segment>
        </Grid.Column>
      </Grid>
    </Fragment>

  )



}
export default NoticeDetails;