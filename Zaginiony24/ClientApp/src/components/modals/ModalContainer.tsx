import React, { useContext } from 'react';
import { Modal } from 'semantic-ui-react';
import { RootStoreContext } from '../../stores/rootStore';
import { observer } from 'mobx-react-lite';

const ModalContainer = () => {
    const rootStore = useContext(RootStoreContext);
    const {modal: {open, body, actions}, closeModal} = rootStore.modalStore;
    return (
        <Modal style={{position:'relative', height:'auto'}} open={open} onClose={closeModal} size='tiny' >
            <Modal.Content>{body}</Modal.Content>
        {actions && (<Modal.Actions>{actions}</Modal.Actions>)}
        </Modal>
    )
}
export default observer(ModalContainer);