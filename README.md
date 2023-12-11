# Proof-of-Work-Implementation
Honours Project - Implementation of the Proof of Work consensus algorithm.

## Objectives
This documentation is used to track the development of a fully functional blockchain system. This will act as a guide and notes for the developer. This documentation might be used as a template for both the user manual and the technical manual. This documentation will be written in Markdown: Here's a link for the basic syntax [Markdown syntax](https://www.markdownguide.org/basic-syntax/).<br>
# Getting started
Visual Studio 2017 or 2019 can be used. At this point, the application is untested on other development environments and focuses on windows systems. Below are the recommended system requirements.

- Windows 10 operating system
- Intel core I5 7th gen / AMD 2nd and 3rd Gen CPU’s
- 8 Gb DDR4 Ram
- 120Gb SSD
- Access to the internet 

1. <u>Downloading the repository:</u><br> 
To clone the repository, make sure git is installed and configured and  in the command prompt,  `git clone --recursive https://github.com/RapsLepeli/Proof-of-Work-Implementation.git`

2. <u>Confugiring the dependencies</u><br>
open the solution file in Visual Studio. The minimum .Net SDK should be `v4.7.2`

3. # User Guide: 

## Chapter 1: Introduction

Chapter 1: Introduction discusses the general overview of the developed system.

### 1.1 Introduction

This project centers around creating a blockchain system with a Proof-of-Work (PoW) consensus
algorithm designed primarily for research and testing purposes. The system comprises several
key components: a distributed blockchain data structure that securely records transactions, a
Peer-to-Peer network to simulate participant interactions, the PoW consensus algorithm to
validate transactions, and the actual transactions themselves, involving secure fund transfers.

The system is engineered with a strong emphasis on the backend, offering a minimal user
interface. It is built using the cross-platform .NET Core framework, specifically designed for
Windows OS for testing purposes. It is independently operable, not intended for integration with
existing blockchain networks, and can handle user errors gracefully without compromising
overall performance. These features make it a valuable tool for understanding and benchmarking
the functionality of blockchain systems and the PoW consensus algorithm.

This project aims to develop a comprehensive, automated system that emulates a fully functional,
permissionless blockchain network. It provides a hands-on opportunity to gain a profound
understanding of blockchain operations, focusing on the PoW consensus algorithm's
implementation and transaction handling. While the project's minimal user interface implies it
targets users who already comprehend Blockchain's purpose, it ensures efficient handling of
transactions, block creation, hash generation, and verification. With its adaptable nature, the
system can run on various machines, though it is optimized for the Windows 10 operating system.
This project's value lies in its potential as a critical research and learning tool for blockchain
enthusiasts and developers.

### 1.2 Summary

Chapter 1: Introduction discusses the general overview of the developed system. Chapter 2:
The installation procedure discusses the installation process of the application.


### Chapter 2: Installation Procedure

Chapter 1: Introduction discussed the general overview of the developed system. Chapter 2:
The installation procedure discusses the installation process of the application.

### 2.1 Introduction

The system created in this project is designed to focus on ease of installation and usage. It
provides an accessible environment for understanding blockchain concepts and the Proof-of-
Work (PoW) consensus algorithm.

### 2.2 Procedure

To install and use the system:

1. The user should ensure the machine has a Windows 10 operating system. This can be done by
searching system information on Windows search, and the following window should be
displayed with the version of Windows currently running on the machine. This is highlighted in
yellow in Figure 2-1:

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/585e9dd4-cf31-4dde-a941-2af267ed3a60)

```
Figure 2-1: Windows Version
```
2. Verify that the machine meets the minimum requirements, including an Intel Core i5 seventh-
gen or AMD 2nd and 3rd Gen CPU, 8 GB DDR4 RAM, 120 GB SSD, and internet access. These
details can be found on the same window from searching system information. This is displayed
in Figure 2-2, highlighted in Yellow.

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/183d07d1-5dce-4e29-a4db-802101ded855)

```
FIGURE 2-2: CPU AND RAM
```

Go to File Explorer and select this PC for physical storage, and Figure 2-3 should be displayed.
This may differ depending on the number of hard drives inside the system:

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/b3e3a680-f401-4f62-8505-fe17ad525e87)

```
FIGURE 2-3: STORAGE
```

3. Download the system developed using the .NET Core framework from the designated source.
In this case, the system can be distributed via a USB flash drive or downloaded from git-hub.

   USB Flash Drive: As the system is distributed along with its source code, the folder
  structure should look as in Figure 2-4:

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/b2ac12ea-8ec6-4d3a-af9b-adef8d70f3c0)

```
FIGURE 2-4: FOLDER STRUCTURE
```
 GitHub(Power Users): follow the link Proof-of-Work-Implementation to the repository
and clone the branch. The user can also follow the installation instructions on the
repository's READ.ME file.

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/5b5c2d01-b75d-4240-95a1-7bfa6c259f04)

```
FIGURE 2-5: GITHUB REPOSITORY
```

The instructions to clone are shown in Figure 2-6:

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/d78a1287-d02f-47a6-8bad-efcbdc0655c4)

```
FIGURE 2-6: READ.ME
```

After the cloning process, the folder structure should look the same as in Figure 2-

4. Follow the instructions and ensure the installation process is successful.


2.3 Summary

Chapter 2: Installation procedure discusses the installation process of the application. Chapter 3
and onwards will give Complete step-by-step explanation of the system grouped by functionality.


## Chapter 3: Launching Application and Peer-to-Peer Network

This chapter focuses on launching and making use of the peer-to-peer network.

A Peer-to-Peer (P2P) network is a decentralized communication model commonly utilized in
blockchain technology. In this network, nodes, represented by participants, collaborate to
exchange information, resources, and services without the need for a central coordinating
authority. Each participant operates as a wallet, serving as their unique identity within the
network. These wallets have a public key for receiving funds and a private key for authorizing
transactions. Each wallet /participant will have their own console user interface in this case.

1. Launch the system on the Windows machine. Since the system uses multiple interfaces, these
steps should be followed to run the system successfully.

```
 Navigate to folder bin > Debug; Figure 3-1 should be displayed.
```
![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/44daacd1-6803-4e72-a4f8-dfe68e0283da)

```
FIGURE 3-1: DEBUG FOLDER
```

```
  Double-click on the PoW.exe file and spawn any number of user interfaces, which should
 be put side by side. The recommended number is 3. An example is shown in Figure 3-
```

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/6404f701-8e0e-4820-9bfd-1f9f58ed8c2a)

```
FIGURE 3-2: LAUNCHED APPLICATION
```

 One of the windows will act as a controller, while the other is a client or wallet.
For the one that will act as a controller, choose option one and press enter on that window.
Then, choose option two for the rest of the windows and press enter. Figure 3-3 and
Figure 3-4 display an example of this:

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/c102daba-816d-465f-b142-ffa1f11b9a4a)

```
FIGURE 3-3: THE CONTROLLER IS CHOSEN.
```
![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/d5730d6f-ad2f-4e51-892c-6f9764aa1f8d)

```
FIGURE 3-4: CLIENTS SELECTED
```

## Chapter 4: Create Blockchain

The process begins when the client initiates the creation of a new blockchain instance. A
singleton instance of the Blockchain is then generated, and within this instance, the Genesis
block is formed with default attributes. This Genesis block serves as the initial block added to
the Blockchain, marking the very beginning of the ledger. Finally, this singleton blockchain
instance is returned to the client, ready for their usage and further interactions with the
Blockchain. This happens the first time when the control application is launched, as shown in
Figure 4-

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/d7524485-ea59-43cf-b589-b707a00b4671)

```
FIGURE 4-1: BLOCKCHAIN CREATION
```

## Chapter 5: Create wallet.

When the clients join the network, the create wallet functionality is carried out: In this process,
the client first interacts with the wallet class to create its unique wallet instance. This wallet
instance includes two crucial components: public and private keys. However, it is essential to
note that only the public key is accessible to the client, while the private key remains hidden and
confidential. This is shown in Figure 5-

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/81905da1-9ef5-4e36-af7b-43a3e7a883d8)

```
FIGURE 5-1: CREATE A WALLET.
```

## Chapter 6: Create Transaction

The client initiates a transaction request to transfer funds from one user to another. This creates
a new transaction, with specific details provided, including the payer, payee, and the amount to
be transferred. Crucially, the payer and payee involved in the transaction are identified by their
respective public keys, ensuring secure and accurate transaction processing. Since this is a
simulation of how the controller application carries out the Blockchain transaction creation, this
is achieved by typing create and pressing enter in the controller application. This is shown in
Figure 6-

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/b17f221a-c44a-45c7-8ace-2063e381e095)

```
FIGURE 6-1: CREATE TRANSACTION.
```

This will auto-generate transactions instead of entering the transactions one by one and manually.


## Chapter 7: Sign Transaction

The client signs the transaction by generating a hash using the payer's private key to secure it.
Notably, the private key is exclusively used to authorize spending within the system and remains
confidential. The signed transaction, bearing this cryptographic signature, can then be verified
by others using the payer's public key, ensuring the transaction's authenticity and integrity. Since
this is a simulation of how the Blockchain works, transaction signing is carried out by the
controller application: this is achieved by typing sign and pressing enter in the controller
application. This is shown in Figure 7-1:

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/078530e0-44a1-4241-a5fe-23afd32370d8)

```
FIGURE 7-1: TRANSACTION SIGNING
```

This will automatically sign all transactions generated in the create transaction functionality.

## Chapter 8: Mining Process

This chapter incorporates the rest of the system's functionalities as the application is created in a
way that automatically completes all the necessary steps. All these runs in the backend and
cannot be displayed via a user interface:

### 8.1 Submit Transaction to Network

The client submits the signed transaction to the system for processing. This transaction is
subsequently relayed through the Peer-to-Peer network to ensure its verification. The transaction
is conveyed alongside the payer's public key, crucial in the verification process. The system
verifies the transaction's authenticity using the payer's public key, assuring the accuracy and
legitimacy of the transaction before it is added to the Blockchain.

### 8.2 Receive Transaction

The client submits the signed transaction to the system for processing. Simultaneously, members
of the peer-to-peer network are notified that a new signed transaction has been submitted. This
transaction includes the payer's public key, a critical element for verifying and maintaining
transaction security within the network.

### 8.3 Verify Transaction

In this process, the client initiates a transaction by submitting a signed transaction to the system.
This action triggers a notification to the members of the peer-to-peer network, informing them
of the newly submitted transaction, which also includes the payer's public key. Subsequently, the
transaction undergoes a verification process to determine its validity within the network,
ensuring the accuracy and security of the transaction.

### 8.4 Add Transaction

Once the transaction has been successfully verified, it is submitted to the system for processing.
A member of the peer-to-peer network takes the responsibility of adding the verified transaction
to the list of current or pending transactions, ensuring its inclusion in the blockchain network.

### 8.5 Create Block

Following a specific session, the client initiates the creation of a new block within the
Blockchain. This new block is generated by incorporating various elements, including the current
time, the payer and payee information, the previous block's hash, and a list of transactions.
Additionally, the block features a signature, and the payer's public key, which must be verified
before the block can be added to the Blockchain.


Furthermore, as part of the process, the blockchain system retrieves the last block from the
existing chain and designates the new block's previous hash to be the same as the last block's
hash. This action ensures the continuity and consistency of the Blockchain by linking new blocks
to the previous ones, maintaining the chronological order of transactions.

8.6 Send Mine Request

In this scenario, the client has created a block that includes a list of transactions. Subsequently,
the client sends a message to the peer-to-peer network, requesting the mining of this block. The
block and the specified difficulty level for the mining process are then submitted to the system.

Mining is an essential process in the Blockchain, involving verifying and adding new blocks to
the chain. Miners compete to solve a challenging mathematical problem, and the first to solve it
is rewarded. The difficulty level determines the complexity of this problem, and it ensures that
the Blockchain remains secure and resistant to fraud.

8.7 Receive Mine Request

In this sequence, the client initiates by sending a message to the peer-to-peer network.
Subsequently, all participants within the peer-to-peer network receive this alert. Following the
alert, each participant takes on the task of mining the block as part of the consensus process.

8.8 Mine Block

When the client requests to create a new block, miners are alerted to initiate the mining process.
Here is how it unfolds:

1. Each miner calculates the hash by applying the difficulty level specified by the client, and they
each attempt to find the exact hash that meets this specific difficulty.
2. The first miner to successfully solve the challenge broadcasts the nonce they discovered,
indicating that they have found the correct solution.
3. Upon receiving this broadcast, the other miners cease their mining activities and focus on
verifying whether the nonce provided is indeed correct.
4. The miner who solved the challenge is rewarded if the nonce is verified as correct. This
typically involves receiving cryptocurrency tokens or rewards for their contribution to the
blockchain network.
5. With the successful completion of this session, the mining process for that block is concluded,
and the Blockchain is updated with the newly mined block.


Since this is a simulation of how the Blockchain works, transaction signing is carried out by the
controller application: this is achieved by typing mine <difficulty> where the <difficulty>
denotes the difficulty [1-5] where 1 is the easiest difficulty, and 5 is the highest difficulty allowed
by the system. The difficulty is capped between 1- 5 as PoW uses a brute force hash generation
method, which consumes a lot of system resources. This is shown in Figure 8-1:

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/4a051763-242f-4327-894c-f889d2c80a76)

```
FIGURE 8-1: MINING
```
8.9 Send Blockchain Message

In the blockchain network, when a member of the Peer-to-Peer network potentially solves the
mathematical challenge, they immediately halt their mining activities. At this point, they send an
alert or notification to inform the other network members that they have successfully found a
valid hash for the current block. This communication is crucial for coordinating the consensus
process and ensuring that the first miner to solve the challenge is recognized and rewarded for
their efforts.

8.10 Receive Blockchain Message

In the blockchain network, when a member of the Peer-to-Peer network successfully mines a
block, they notify the entire network to announce that the block has been mined. Subsequently,
all network members stop their mining activities and initiate a verification process to determine
whether the mined block is correct. This verification step is essential to ensure the accuracy and
integrity of the Blockchain, and it involves confirming that the mined block meets the specified
criteria and is valid according to the consensus rules.


8.11 Verify Block

In this blockchain process:

1. The client submits the mined block to the system after it has been successfully mined.
2. An alert is sent to the members of the peer-to-peer network, notifying them that a newly mined
block has been submitted.
3. Other members of the network then engage in a verification process to confirm the validity of
the mined block. This verification ensures that the block adheres to the network's consensus rules
and is consistent with the requirements for inclusion in the Blockchain.
4. After successful verification, the block is returned to the system, where it is added to the
Blockchain, contributing to the continuation of the ledger.

8.12 Reward Miner

In the blockchain network, when a miner successfully solves the mathematical challenge:

1. The mined and verified block is added to the Blockchain, marking this session's completion
and the new block's inclusion in the ledger.
2. A new transaction is created to reward the miner for their efforts. This transaction specifies
the miner's address and the reward they are entitled to receive. This transaction is added to the
pending transactions list as the first entry.
3. The miner is rewarded in the subsequent session. This typically involves the miner receiving
the cryptocurrency tokens or rewards specified in the transaction as their mining reward. This
process incentivizes miners to participate in the blockchain network's security and maintenance.

8.13 Add Block

In the blockchain network, when a peer-to-peer member receives a mined and verified block:

1. The member adds the block to the singleton main blockchain, incorporating the new data into
the ledger.
2. Once the Blockchain has been updated with the newly added block, it is returned to the client.
This ensures the client has access to the most up-to-date version of the Blockchain.
3. Simultaneously, the list of pending transactions is reset to zero, as these transactions have now
been included in the Blockchain.


4. The client is notified that the new block has been successfully added to the Blockchain,
providing real-time information and confirmation of the transaction's completion.

8.14 Display Blockchain Contents

In this interaction between the client and the Blockchain:

1. The client initiates a request to the Blockchain, explicitly asking for information on all
recorded transactions.
2. The Blockchain processes the request and returns the complete contents of the Blockchain,
including details of all the transactions stored within the ledger.
3. The client then displays the contents returned, providing a comprehensive view of all the
transactions and related information, allowing users to access and review the complete
transaction history stored in the Blockchain.

Since this is a simulation of how the Blockchain works, displaying the Blockchain is carried out
by the controller application: this is achieved by typing display and pressing enter. Figure 8-
will be displayed.

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/7a432d1e-d4de-4f38-8a4f-5fe01a9590b6)

```
FIGURE 8-2: DISPLAY BLOCKCHAIN
```

2. The user interface may be minimal but provides essential functionalities.
3. The system will simulate a permissionless blockchain network where the user can interact
with wallets representing participants.


4. Create transactions by specifying the transfer amount, payer, and payee amount.
5. The system will handle the transaction hashing, signing, and verification.
6. The Proof-of-Work (PoW) consensus algorithm will validate these transactions by solving
complex mathematical challenges.
7. In chronological order, the system will add these validated transactions to the blockchain data
structure.

8.15 Display Mining Statistics

This functionality Figure 8-3 allows the tester to display all Blockchain mining statistics such as
the number of tries each block took to be mined and the time it took for each block to be mined.
This functionality allows testers to measure the efficiency of the PoW algorithm and use it as a
benchmark for the proposed Proof of Publicly Verifiable Randomness (PoPVR) algorithm.

![image](https://github.com/RapsLepeli/Proof-of-Work-Implementation/assets/50484534/bb6a327a-fa5f-460c-a0e8-812f30ed392e)

```
FIGURE 8-3: MINING STATS
```

8.16 Summary

In summary, the series of interactions and processes described provide a comprehensive
overview of the functioning of a blockchain system. From the initial creation of a blockchain


instance to transaction handling, mining, and rewards, the steps illustrate the critical components
and steps involved in a blockchain's operation.

Key elements such as consensus, verification, peer-to-peer coordination, and client interactions
underscore the importance of transparency, security, and decentralization in blockchain
technology. This sequence of actions demonstrates how blockchain systems maintain trust and
integrity in recording and verifying transactions, ultimately serving as a foundational technology
with widespread applications in various industries.


### Chapter 9: Conclusion

The primary purpose of this system is to provide a hands-on experience with blockchain
operations and the PoW algorithm, making it an excellent resource for those interested in
learning about blockchain technology and consensus mechanisms. Its ease of installation and
straightforward usage ensure that users can quickly delve into Blockchain without unnecessary
complexities.

Moreover, this system is highly customizable and adaptable. During runtime, the user can
configure the number of wallets or participants, allowing for various scenarios and testing
conditions. This flexibility can help users better understand how different network sizes and
dynamics can impact a blockchain system's performance.

While the system's user interface may be minimal, it is designed to focus on the critical aspects
of blockchain functionality. It automates many processes, reducing the need for extensive human
interaction, but it handles user errors gracefully. For instance, if a user attempts to transfer funds
they do not possess, the system will alert them, ensuring the integrity of transactions.

Overall, the system is an invaluable tool for those looking to experiment with blockchain
technology, learn about the PoW consensus algorithm, and gain practical insights into blockchain
networks' operation. Its user-friendly installation and usage make it accessible to a wide range
of users, from blockchain enthusiasts to developers and researchers seeking to explore the world
of Blockchain in a hands-on manner.



 
  This project aims to implement a Blockchain Proof-of-Work (PoW) consensus algorithm. A simple blockchain system will be created from scratch to create, validate, and complete transactions. The blockchain system implemented will include a data structure to record the transactions, the PoW consensus algorithm, a transaction, and a simple peer-to-peer network that simulates nodes or computers participating in the transaction; this includes the sender, receiver, and block proposers. 
  
# Deliverables and Componets
These will change and evolve as time goes by
>	**The Distributed Blockchain data structure**: A distributed blockchain data structure. The blockchain data structure is an ordered, back-linked list of blocks containing transactions. It can be stored in a simple database or as a file. A hash generated by the cryptographic hash technique SHA256 on the block header identifies each block. For example, in the “prev block hash” field of the block header, each block refers to a preceding block called the parent block . 
SHA256 stands for Secure hash algorithm with a length of 256 bits. It was developed by the US’s National Security Agency in 2001 and is known as a one-way cryptographic function. This means it can encrypt data but not decrypt it back to its original form. Each block contains the previous hash block and its hash . A single block also contains a timestamp of when it was created so that all blocks in the chain are placed chronologically. Before a block is added to the blockchain, it is verified by other participants in the system. This ledger records all transactions in this system; only a single instance exists. The figure below shows what a general blockchain data structure looks like

>
>	**A usable Transaction entity.**: A transaction. Refers to the action of transferring funds from one participant to another. A Transaction consists of the amount(can be Bitcoin, Rapscoin, or just plain money) transferred, the payer and the payee. 
>
>	**A simple functional Peer-to-Peer(P2P) network:**:	A Peer-to-Peer network. Message passing on a blockchain network occurs over a peer-to-peer (P2P) network; P2P is a well-known network topology. P2P refers to a decentralised topology where nodes collaborate to share resources and services without a central authority to coordinate the processes . Each participant is a wallet, and this wallet defines the participant’s identity. Each wallet has a public key(for receiving money) and a private key(for spending money). The Pubic-Private key pair is generated using a hashing technique called RSA(Rivest-Shamir-Adleman). Unlike SHA256, this full encryption algorithm can encrypt and decrypt data if one has the proper key .
To encrypt data, a public key is used. It converts the data into ciphertext, an unreadable version of the original data. The corresponding private key can fully decrypt the data back to its original form. However, the Pubic-Private key pair creates a digital signature in this instance. Instead of encrypting the data(Transaction), a hash of that data is created and sign the hash using a private key. Others can then verify the data by using the public key of the person who initiated it to check if it is valid. This means if by any chance another participant tempers with transaction data, verification will not work as the transaction is untrustworthy. This means no one can temper the transaction message since it was signed. The wallet can receive money and spend/send money. The developed simple P2P network simulates how it would function in a blockchain system. The figure below shows how a general P2P network looks like

>
>	**A Functional Proof-of-Work Consensus algorithm.**: A consensus algorithm: Proof-of-Work (PoW) is the mechanism of choice for most cryptocurrencies  and in this project. The algorithm will verify transactions and add blocks to the blockchain data structure. This consensus forces each new block to go through a mining process, where a difficult mathematical challenge is solved to confirm that the block is valid. Once this problem has been solved, it is effortless to verify. Multiple nodes/participants compete to solve this challenge, which works like a lottery. The winner of the lottery gets rewards for solving the complex problem. This process is very resource intensive and inefficient and takes a long time to complete. The problem states that:  find a hash for this block, and this hash must at least start with five zeros or any number. The only way to find this is through brute force and multiple hash computations. The system or the tester automatically decides the problem’s difficulty. The hash is calculated using the MD5(Message-Digest) Algorithm. It is very similar to the SHA256 technique but faster and only 128 bits in length. The figure below shows the results of PoW, where the correct block proposer solves a hash calculation before other network participants.
>
>	**A simple Minimal interface.** The interface should allow the user to set the number of network nodes, start a transaction and display notifications when transactions are completed or created.

Make sure to strike through each link after it has been used :fire:.

## <u>The Dstributed Blockchain data structure</u><br>

A distributed blockchain data structure. The blockchain data structure can be defined as an ordered, back-linked list of blocks containing transactions. It can be stored in a simple database or as a file. A hash-generated cryptographic hash technique on the block header identifies each block. For example, in the "prior block hash" field of the block header, each block refers to a preceding block called the parent block . This ledger will be used to record all transactions in this system. The figure below shows what a general blockchain data structure looks like ![Basic Structure](/assets/Structureofblocksinblockchain.png)

Resources that will be used as references are listed below<br>
~~- [Geeks for Geeks Java Implementation](https://www.geeksforgeeks.org/implementation-of-blockchain-in-java/?ref=rp)~~<br>
~~- [Write your Own Proof-of-Work Blockchain](https://www.jmeiners.com/tiny-blockchain/)~~
- [Blockchain and Proof of Work](https://marceloh-web.medium.com/blockchain-and-proof-of-work-f35ffc33c459)
- [Building a Blockchain in Under 15 Minutes - Programmer explains](https://www.youtube.com/watch?v=baJYhYsHkLM)
- [Bitcoin ₿ in 100 Seconds // Build your Own Blockchain](https://www.youtube.com/watch?v=qF7dkrce-mQ)
- [Build Blockchain from Scratch](https://youtube.com/playlist?list=PLCQ3cvOTrX6DlfYPigIw4QiNnE3wnoAxR)
- [Build Your Own Blockchain Using Python](https://www.youtube.com/watch?v=Jt9MYcSsVzs)
- [Coding A Blockchain in Python](https://www.youtube.com/watch?v=pYasYyjByKI)
- [Creating a blockchain with Javascript (Blockchain, part 1)](https://www.youtube.com/watch?v=zVqczFZr124)
- [Blockchain from Scratch in Python Tutorial](https://www.youtube.com/watch?v=alNU9AVWkQk)
- [Build a Blockchain with Python & FastAPI](https://www.youtube.com/watch?v=G5M4bsxR-7E)
- [Blockchain Programming Development with Python and Firebase](https://www.youtube.com/watch?v=DFP9x5QS8tk)
- [Create a smple Blockchain  in Python](https://www.youtube.com/@SkoloOnline/search?query=blockchain)
- [Building a Blockchain in 15 Minutes (Python) - Programmer explains](https://www.youtube.com/watch?v=4FwBB6vhilU&pp=ygUaY3JlYXRlIGEgc21wbGUgQmxvY2tjaGFpbiA%3D)
- [Simple Blockchain in 5 Minutes](https://www.youtube.com/watch?v=MViBvQXQ3mM)
- [Blockchain explained using C# implementation](https://towardsdatascience.com/blockchain-explained-using-c-implementation-fb60f29b9f07)
- [Singleton Design Pattern In C#](https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/)
- [Building A Blockchain In .NET Core - Basic Blockchain (+ Diagrams)](https://www.c-sharpcorner.com/article/blockchain-basics-building-a-blockchain-in-net-core/)
- [Simplest Blockchain in C#](https://dev.to/amir_ashy/simplest-blockchain-in-c-1f70)
- [Blockchain - Important](https://www.youtube.com/watch?v=V6lqdJPDzI0&list=PLXLkA7FAishohjZkwmPTACpCIXKvc7Fyl&index=4&ab_channel=Hacked)
- [blockchain - Important 2](https://putukusuma.medium.com/developing-simple-crypto-application-using-c-48258c2d4e45)

## <u>Proof of Work</u><br>
- [Building A Blockchain In .NET Core - Proof Of Work (+ Diagrams)](https://www.c-sharpcorner.com/article/building-a-blockchain-in-net-core-proof-of-work/)

## <u>Transaction and Reward Mechanism</u><br>

- [Building A Blockchain In .NET Core - Transaction And Reward + (Diagrams)](https://www.c-sharpcorner.com/article/building-a-blockchain-in-net-core-transaction-and-reward/)
- [Merkle root of a bitcoin block calculated in C#](https://medium.com/coinmonks/merkle-root-of-a-bitcoin-block-calculated-in-c-8c659a3b3290)

## <u>Peer to Peer Network</u><br>

- [Building A Blockchain In .NET Core - P2P Network(Starting Point)](https://www.c-sharpcorner.com/article/building-a-blockchain-in-net-core-p2p-network/)
